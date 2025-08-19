using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RMSPrivateServerAPI.Data;
using RMSPrivateServerAPI.DTOs;
using RMSPrivateServerAPI.Entities;
using RMSPrivateServerAPI.Enums;
using RMSPrivateServerAPI.Interfaces;
using RMSPrivateServerAPI.Models;
using RMSPrivateServerAPI.Services;
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
        /// ѕолучение текущей задачи дл€ робота
        /// </summary>
        /// <param name="robotId">Id робота</param>
        /// <returns></returns>
        [HttpGet("{robotId}/current/")]
        public async Task<ActionResult<RobotTaskDto?>> GetCurrentTask(Guid robotId)
        {
            var (wmsTask, wmsAction) = _robotTaskService.RobotTaskActions(robotId);

            Queue<RobotAction> robotActions = await _robotTaskService.GetRobotActions(robotId);

            if (robotActions == null || wmsTask == null || wmsAction == null)            
                return NotFound();


            List<PathDto> paths = await _pointService.GetPathElementsWithTypesAndPathsAsync();

            foreach (PathDto path in paths)
            {
                var pt_from =  _context.Points.FirstOrDefault(q => q.Id == path.StartId);

                var pt_to = _context.Points.FirstOrDefault(q => q.Id == path.FinishId); ;
            }

            List<PointDto> points = await _pointService.GetPointsWithTypesAsync();

            MoveArcAction moveArcAction = new MoveArcAction();
            robotActions.Enqueue(moveArcAction);

            MoveLinearAction moveLinearAction = new MoveLinearAction();
            robotActions.Enqueue(moveArcAction);

            TurnAction turnAction = new TurnAction();
            robotActions.Enqueue(turnAction);

            StopAction stopAction = new StopAction();
            robotActions.Enqueue(stopAction);



            RobotTaskDto robotTaskDto = new RobotTaskDto()
            {
                TaskId = wmsTask.TaskId,
                RobotId = robotId,
                Title = $"Area: {wmsTask.AreaWmsId}, Location: {wmsAction.Location}",
                RobotActions = new Queue<RobotAction>()
            };

            robotTaskDto.RobotActions = robotActions;

            //robotTaskDto.RobotActions.Enqueue(new MoveToAction () { ActionTypeId = ActionType.moveTo, ActionName = "Go", Pose = new Pose() {X=0, Y=0, Heading = 1 } }) ;

            return Ok(robotTaskDto);
        }


        /// <summary>
        /// ѕолучение текущей задачи дл€ робота
        /// </summary>
        /// <param name="robotId">Id робота</param>
        /// <returns></returns>
        [HttpGet("{robotId}/cur/")]
        public async Task<ActionResult<RobotTaskDto>> GetRobotTaskCurrent(Guid robotId)
        {
            List<RobotTaskFlat?> robotTask = await _robotTaskService.GetCurrent(robotId);

            if (robotTask == null)
            {
                return NotFound();
            }

            RobotTaskDto robotTaskDto = _mapper.Map<RobotTaskDto>(robotTask);

            return robotTaskDto;
        }

        /// <summary>
        /// –обот кидает событие в момент завершени€ текущей операции (подзадачи), 
        /// сервер должен прин€ть это к сведению и ответить, двигатьс€ ли роботу дальше или подождать, 
	    /// а может, вообще, планы кардинально изменились и нужно заново запрашивать весь список
        /// </summary>
        /// <param name="robotID">Id робота</param>
        /// <param name="request">–езультат выполнени€ текущей операции</param>
        /// <returns>–обот завершил текущую операцию</returns>
        [HttpPost("{robotID}/action-done")]
        public IActionResult ActionDone(Guid robotID, [FromBody] ActionDoneRequest request)
        {
            try
            {
                // ќбработка завершени€ операции
                TasksDto? tasks = _context.Tasks.Find(request.TaskId);

                //if (task.actions[request.ActionIndex].ActionType == 0)
                //{
                //    // Ћогика обработки ошибки
                //    return Ok(new { command = "abort" });
                //}

                //return Ok(new { command = "next", action = task.actions[request.ActionIndex + 1] });
                //
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
        /// <param name="returnDeletedRecords">If true, the method will return all the records, including the ones that have been deleted</param>
        /// <response code="200">Robot task returned</response>
        /// <response code="404">Specified task not found</response>
        /// <response code="500">An Internal Server Error prevented the request from being executed.</response>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        /// <summary>
        /// ѕолучение списка задач робота
        /// </summary>
        /// <param name="robotId">Id робота</param>
        /// <returns></returns>
        [HttpGet("{robotId}/tasks/all/")]
        public async Task<IEnumerable<robot_task>> GetAll(Guid robotId)
        {
            return await _robotTaskRepository.GetAll(robotId);
        }


        /// <summary>
        /// ѕолучить задачу по ее уникальному коду
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
        /// ƒобавить задачу
        /// </summary>
        /// <param name="robotTaskAsDto"></param>
        /// <returns></returns>
        [HttpPost("Add")]
        public async Task<ActionResult<robot_task>> Insert([FromBody] RobotTaskDto robotTaskAsDto)
        {
            try
            {
                if (robotTaskAsDto == null)
                {
                    return BadRequest("No Robot Task was provided");
                }

                robot_task robotTaskToInsert = _mapper.Map<robot_task>(robotTaskAsDto);

                var insertedRobotTask = await _robotTaskService.Insert(robotTaskToInsert);

                var insertedRobotTaskDto = _mapper.Map<RobotTaskDto>(insertedRobotTask);

                var location = $"https://localhost/RobotTask/{insertedRobotTaskDto.TaskId}";

                return Created(location, insertedRobotTaskDto);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// –едактировать задачу
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
        /// ”далить задачу
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


        [HttpPost("AddRobotAction/{taskId}")]
        public async Task<IActionResult> AddRobotAction(Guid taskId, [FromBody] RobotActionsDto robotActionDto)
        {
            if (robotActionDto == null)
            {
                return BadRequest("Invalid data.");
            }

            var robotAction = new RobotActionsDto
            {
                ActionId = Guid.NewGuid(),
                TaskId   = taskId,
                Title    = robotActionDto.Title,
                ActionType = robotActionDto.ActionType,
                Pose_X = robotActionDto.Pose_X,
                Pose_Y = robotActionDto.Pose_Y,
                Heading = robotActionDto.Heading,
                Direction = robotActionDto.Direction,
                Distance = robotActionDto.Distance,
                Angle = robotActionDto.Angle,
                Radius = robotActionDto.Radius
            };

            _context.RobotActions.Add(robotAction);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(AddRobotAction), new { id = robotAction.ActionId }, robotAction);
        }
    }
}
