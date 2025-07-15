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

        [HttpGet]
        public async Task<IEnumerable<RobotTask>> GetAll()
        {            
            return await _robotTaskRepository.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RobotTask>> Get(int id)
        {
            var robotTask = await _robotTaskService.Get(id);
            if (robotTask == null)
            {
                return NotFound();
            }
            return robotTask;
        }

        [HttpPost]
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

                var location = $"https://localhost:5001/RobotTask/{insertedRobotTaskDto.TaskId}";

                return Created(location, insertedRobotTaskDto);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpPut]
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
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
