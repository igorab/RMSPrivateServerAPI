using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RMSPrivateServerAPI.Data;
using RMSPrivateServerAPI.DTOs;
using RMSPrivateServerAPI.Entities;
using RMSPrivateServerAPI.Interfaces;
using RMSPrivateServerAPI.Models;
using System.Net.Mime;
using System.Threading.Tasks;
#pragma warning disable CS1591
namespace RMSPrivateServerAPI.Controllers
{
    [ApiController]    
    [Route("api/v1.0/[controller]/")]
    public class RobotTaskController : ControllerBase
    {       
        private readonly ILogger<RobotTaskController> _logger;
        private readonly IRobotTaskRepository _robotTaskRepository;
        private readonly IRobotTaskService _robotTaskService;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;


        public RobotTaskController(ILogger<RobotTaskController> logger, 
                                   IRobotTaskRepository repository, 
                                   IRobotTaskService service, 
                                   IMapper mapper,
                                   ApplicationDbContext context
                                   )
        {
            _logger = logger;
            _robotTaskRepository = repository;
            _robotTaskService = service;
            _mapper = mapper;
            _context = context;
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
        /// ��������� ������ ����� ������
        /// </summary>
        /// <param name="robotId">Id ������</param>
        /// <returns></returns>
        [HttpGet("{robotId}/tasks/all/")]
        public async Task<IEnumerable<robot_task>> GetAll(string robotId)
        {
            return await _robotTaskRepository.GetAll(robotId);
        }


        /// <summary>
        /// �������� ������ �� �� ����������� ����
        /// </summary>
        /// <param name="taskId">Id ������</param>
        /// <returns></returns>
        [HttpGet("{taskId}/")]
        public async Task<ActionResult<RobotTaskDto>> GetById(string taskId)
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
        /// ��������� ������� ������ ��� ������
        /// </summary>
        /// <param name="robotId">Id ������</param>
        /// <returns></returns>
        [HttpGet("{robotId}/tasks/current/")]
        public async Task<ActionResult<RobotTaskDto>> GetCurrentTask(string robotId)
        {
            var robotTask = await _robotTaskService.GetCurrent(robotId);

            if (robotTask == null)
            {
                return NotFound();
            }

            var robotTaskDto = _mapper.Map<RobotTaskDto>(robotTask);

            return robotTaskDto;
        }

        /// <summary>
        /// ����� ������ ������� � ������ ���������� ������� �������� (���������), 
        /// ������ ������ ������� ��� � �������� � ��������, ��������� �� ������ ������ ��� ���������, 
	    /// � �����, ������, ����� ����������� ���������� � ����� ������ ����������� ���� ������
        /// </summary>
        /// <param name="robotID">Id ������</param>
        /// <param name="request">��������� ���������� ������� ��������</param>
        /// <returns>����� �������� ������� ��������</returns>
        [HttpPost("{robotID}/tasks/action-done")]
        public IActionResult ActionDone(string robotID, [FromBody] ActionDoneRequest request)
        {
            try
            {
                // ��������� ���������� ��������
                robot_task? task = _context.Tasks.Find(request.TaskId);

                //if (task.actions[request.ActionIndex].ActionType == 0)
                //{
                //    // ������ ��������� ������
                //    return Ok(new { command = "abort" });
                //}

                //return Ok(new { command = "next", action = task.actions[request.ActionIndex + 1] });
                //
                return Ok(request);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            };
        }      

        /// <summary>
        /// �������� ������
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

                var robotTaskToInsert = _mapper.Map<robot_task>(robotTaskAsDto);

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
        /// ������������� ������
        /// </summary>
        /// <param name="robotTask"></param>
        /// <returns></returns>
        [HttpPut(("Edit"))]
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
        /// ������� ������
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(string id)
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
