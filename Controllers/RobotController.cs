using AutoMapper;
using RMSPrivateServerAPI.DTOs;
using RMSPrivateServerAPI.Entities;
using RMSPrivateServerAPI.Interfaces;
using RMSPrivateServerAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace RMSPrivateServerAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class RobotController : ControllerBase
{
    private readonly ILogger<RobotController> _logger;
    private readonly IRobotRepository _robotRepository;
    private readonly IRobotService _robotService;
    private readonly IMapper _mapper;

    public RobotController(ILogger<RobotController> logger,
                           IRobotRepository robotRepository,
                           IRobotService robotService,
                           IMapper  mapper)
                            
    {
        _logger = logger;
        _robotRepository = robotRepository;
        _robotService = robotService;
        _mapper = mapper;
    }


    /// <summary>
    /// Get all the robots in the Database 
    /// </summary>
    /// <param name="returnDeletedRecords">If true, the method will return all the records</param>     
    [HttpGet]
    public async Task<IEnumerable<robotinfo>> GetAll([FromRoute] bool returnDeletedRecords = false)
    {        
        return await _robotRepository.GetAll(returnDeletedRecords);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<robotinfo>> Get(int id)
    {
        var robot = await _robotService.Get(id);
        if (robot == null)
        {
            return NotFound();
        }
        return robot;
    }

    [HttpPost]
    public async Task<ActionResult<robotinfo>> Insert([FromBody] RobotInfoDto robotAsDto)
    {
        try
        {
            if (robotAsDto == null)
            {
                return BadRequest("No Robot was provided");
            }

            var robotToInsert = _mapper.Map<robotinfo>(robotAsDto);

            var insertedRobot = await _robotService.Insert(robotToInsert);

            var insertedRobotDto = _mapper.Map<RobotInfoDto>(insertedRobot);

            var location = $"https://localhost:5001/RobotInfo/{insertedRobotDto.RobotId}"; ;

            return Created(location, insertedRobotDto);

        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] robotinfo robot)
    {
        try
        {
            await _robotService.Update(robot);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _robotService.Delete(id);
        }
        catch (Exception e)
        { 
            return BadRequest(e);
        }

        return NoContent();
    }

    //[HttpGet("List")]
    //public ActionResult GetRobots()
    //{
    //    var robots = _robotService.GetAllRobots();

    //    return Ok(robots);
    //}

    //[HttpPost("Add")]
    //public ActionResult AddRobot([FromBody] RobotInfoDto  robotDto)
    //{
    //    _robotService.AddRobot(robotDto);

    //    return CreatedAtAction(nameof(GetRobots), new {id = robotDto.Id }, robotDto); 
    //}

    //[HttpPost("Edit")]
    //public ActionResult EditRobot([FromBody] RobotInfoDto robotDto)
    //{
    //    _robotService.EditRobot(robotDto.Id, robotDto);
    //    return NoContent();
    //}
}
