using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RMSPrivateServerAPI.DTOs;
using RMSPrivateServerAPI.Entities;
using RMSPrivateServerAPI.Interfaces;
using System.Threading.Tasks;

namespace RMSPrivateServerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RobotTaskController : ControllerBase
    {       
        private readonly ILogger<RobotTaskController> _logger;
        private readonly IRobotTaskRepository _robotTaskRepository;
        private readonly IRobotTaskService _robotTaskService;
        private readonly IMapper _mapper;


        public RobotTaskController(ILogger<RobotTaskController> logger, 
                                   IRobotTaskRepository repository, 
                                   IRobotTaskService service, 
                                   IMapper mapper)
        {
            _logger = logger;
            _robotTaskRepository = repository;
            _robotTaskService = service;
            _mapper = mapper;
        }

        /// <summary>
        /// Получение списка задач робота
        /// </summary>
        /// <param name="robotId">Id робота</param>
        /// <returns></returns>
        [HttpGet("{robotId}/tasks/all/")]
        public async Task<IEnumerable<RobotTask>> GetAll(string robotId)
        {            
            return await _robotTaskRepository.GetAll(robotId);
        }

        /// <summary>
        /// Получение текущей задачи для робота
        /// </summary>
        /// <param name="robotId">Id робота</param>
        /// <returns></returns>
        [HttpGet("{robotId}/tasks/current/")]
        public async Task<ActionResult<RobotTask>> GetCurrentTask(string robotId)
        {
            var robotTask = await _robotTaskService.Get(robotId);
            if (robotTask == null)
            {
                return NotFound();
            }
            return robotTask;
        }

        /// <summary>
        /// Добавить задачу
        /// </summary>
        /// <param name="robotTaskAsDto"></param>
        /// <returns></returns>
        [HttpPost("Add")]
        public async Task<ActionResult<RobotTask>> Insert([FromBody] RobotTaskDto robotTaskAsDto)
        {
            try
            {
                if (robotTaskAsDto == null)
                {
                    return BadRequest("No Task was provided");
                }

                var robotTaskToInsert = _mapper.Map<RobotTask>(robotTaskAsDto);

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
        /// Редактировать задачу
        /// </summary>
        /// <param name="robotTask"></param>
        /// <returns></returns>
        [HttpPut(("Edit"))]
        public async Task<IActionResult> Put([FromBody] RobotTask robotTask)
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
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _robotTaskService.Delete(id);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
            return NoContent();
        }
    }
}
