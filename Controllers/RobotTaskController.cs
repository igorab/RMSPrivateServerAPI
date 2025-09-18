using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RMSPrivateServerAPI.Data;
using RMSPrivateServerAPI.DTOs;
using RMSPrivateServerAPI.Entities;
using RMSPrivateServerAPI.Enums;
using RMSPrivateServerAPI.Interfaces;
using RMSPrivateServerAPI.Models;
using RMSPrivateServerAPI.Models.Lib;
using RMSPrivateServerAPI.Services;
using RMSPrivateServerAPI.StoreMapDto;
using System.Collections;
using System.Net.Mime;
using System.Threading.Tasks;
#pragma warning disable CS1591
namespace RMSPrivateServerAPI.Controllers
{
    [ApiController]    
    [Route("api/v1/[controller]/")]
    public class RobotTaskController : ControllerBase
    {       
        private readonly ILogger<RobotTaskController> _logger;
        private readonly IRobotTaskRepository _robotTaskRepository;
        private readonly IRobotTaskService _robotTaskService;

        private readonly PointService _pointService;
        private readonly IMapper _mapper;
        private readonly WmsDbContext _context;

        public RobotTaskController(ILogger<RobotTaskController> logger, 
                                   IRobotTaskRepository repository, 
                                   IRobotTaskService service,
                                   PointService pointService,
                                   IMapper mapper,
                                   WmsDbContext context
                                   )
        {
            _logger = logger;
            _robotTaskRepository = repository;
            _robotTaskService = service;
            _mapper = mapper;
            _context = context;
            _pointService = pointService;
        }

        /// <summary>
        /// Получение текущей задачи для робота
        /// </summary>
        /// <param name="robotId">Id робота</param>
        /// <returns></returns>
        [HttpGet("{robotId}/current/")]
        public async Task<ActionResult<RobotTaskDto?>> GetCurrentTask(Guid robotId)
        {
            var robot_tasks = _robotTaskService.GetAll(robotId).Result;
            if (robot_tasks.IsNullOrEmpty()) 
            { 
                return Ok(new RobotTaskDto() {RobotId = robotId  }); 
            }

            (TasksDto? wmsTask, List<TaskActionsDto>? wmsTaskAction) = _robotTaskService.RobotTaskActions(robotId);

            if (wmsTask == null || wmsTaskAction == null)
                return NotFound();

            Queue<RobotAction> robotActions = await _robotTaskService.RobotTaskActionsQueue(robotId, wmsTask.TaskId);
            if (robotActions == null)
                return NotFound();

            RobotTaskDto robotTaskDto = new RobotTaskDto()
            {
                TaskId = wmsTask.TaskId,
                RobotId = robotId,
                Title = $"Area: {wmsTask.AreaWmsId}, Store: {wmsTask.StoreWmsId}",
                QueueRobotActions = new Queue<RobotAction>()
            };

            robotTaskDto.QueueRobotActions = robotActions;
            
            return Ok(robotTaskDto);
        }


        /// <summary>
        /// Получение текущей задачи для робота
        /// </summary>
        /// <param name="robotId">Id робота</param>
        /// <returns></returns>
        [HttpGet("{robotId}/cur/")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult<RobotTaskDto>> GetRobotTaskCurrent(Guid robotId)
        {
            List<RobotActionsDone?> robotTask = await _robotTaskService.GetCurrent(robotId);

            if (robotTask == null)
            {
                return NotFound();
            }

            RobotTaskDto robotTaskDto = _mapper.Map<RobotTaskDto>(robotTask);

            return robotTaskDto;
        }

        /// <summary>
        /// Робот кидает событие в момент завершения текущей операции (подзадачи), 
        /// сервер должен принять это к сведению и ответить, двигаться ли роботу дальше или подождать, 
	    /// а может, вообще, планы кардинально изменились и нужно заново запрашивать весь список
        /// </summary>
        /// <param name="robotId">Id робота</param>
        /// <param name="request">Результат выполнения текущей операции</param>
        /// <returns>Робот завершил текущую операцию</returns>
        [HttpPost("{robotId}/action-done")]
        public async Task<IActionResult> ActionDone(Guid robotId, 
                                                    [FromBody] ActionDoneRequest request)
        {
            bool allActionsDone = false; 

            try
            {
                robot_info? rbt = _context.Robots.Find(robotId);
                if (rbt == null) return NotFound(new { command = nameof(RobotCommand.abort) });

                TasksDto? tasks = null;

                if (request.TaskId != Guid.Empty && 
                    request.ActionIndex >= 0 )
                {                   
                    tasks = _context.Tasks.Find(request.TaskId);                                        
                }

                if (tasks == null) return NotFound(new { command = nameof(RobotCommand.abort) });

                await _robotTaskService.AddRobotAction(robotId, request);

                IEnumerable<robot_task>? robot_tasks = await _robotTaskService.GetAll(robotId);

                allActionsDone = await _robotTaskService.AllActionsDone(robotId, request.TaskId);

                if (allActionsDone)
                {
                    foreach (robot_task rtask in robot_tasks.Where(rt => rt.TaskId == request.TaskId) )
                    {
                        await _robotTaskService.TaskStatusDone(rtask.TaskId);
                    }

                    List<TaskActionsDto>? taskActions = _context.TaskActions.
                        Where(q => q.TaskId == request.TaskId).ToList();

                    foreach (TaskActionsDto taskAction in taskActions)
                    {
                        await _robotTaskService.UpdateTaskActionStatusToCompleted(taskAction.TaskId, taskAction.Id);
                    }

                }
                
                return Ok( new { command = nameof(RobotCommand.next) });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }            
        }


        /// <summary>
        /// Get all the robot tasks in the Database 
        /// </summary>        
        /// <response code="200">Robot task returned</response>
        /// <response code="404">Specified task not found</response>
        /// <response code="500">An Internal Server Error prevented the request from being executed.</response>
        /// <param name="robotId">Id робота</param>
        /// <returns></returns>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]             
        [HttpGet("{robotId}/tasks/all/")]
        public async Task<IEnumerable<robot_task>> GetAll(Guid robotId)
        {
            return await _robotTaskRepository.GetAll(robotId);
        }


        /// <summary>
        /// Получить задачу по ее уникальному коду
        /// </summary>
        /// <param name="taskId">Id задачи</param>
        /// <returns></returns>
        [HttpGet("{taskId}/")]
        public async Task<ActionResult<RobotTaskDto>> GetById(Guid taskId)
        {
            var robotTask = await _robotTaskService.GetById(taskId);

            if (robotTask == null)
            {
                return NotFound();
            }

            var robotTaskDto = _mapper.Map<RobotTaskDto>(robotTask);

            return robotTaskDto;
        }

                              
        /// <summary>
        /// Добавить задачу
        /// </summary>
        /// <param name="robotId">id робота</param>
        /// <param name="taskId">id task</param>
        /// <param name="title">описание</param>
        /// <returns></returns>
        [HttpPut("Add")]
        public async Task<IActionResult> Add(Guid robotId, Guid taskId, string? title)
        {
            try
            {
                robot_task robotTask = new robot_task() {RobotId = robotId, TaskId = taskId, Title = title };

                await _robotTaskService.Update(robotTask);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

            return NoContent();
        }



        /// <summary>
        /// Редактировать задачу
        /// </summary>
        /// <param name="robotTask"></param>
        /// <returns></returns>
        [HttpPut("Edit")]
        public async Task<IActionResult> Put([FromBody] robot_task robotTask)
        {
            try
            {
                await _robotTaskService.Update(robotTask);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);  
            }

            return NoContent();
        }

        /// <summary>
        /// Удалить задачу
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _robotTaskService.DeleteTask(id);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
            return NoContent();
        }
        
    }
}
