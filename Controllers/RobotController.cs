using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RMSPrivateServerAPI.DTOs;
using RMSPrivateServerAPI.Entities;
using RMSPrivateServerAPI.Interfaces;
using RMSPrivateServerAPI.Models;
using RMSPrivateServerAPI.Repositories;
using RMSPrivateServerAPI.Services;

namespace RMSPrivateServerAPI.Controllers;

[ApiController]
[Route("api/Robot/v1.0/robot")]
public class RobotController : ControllerBase
{
    private readonly ILogger<RobotController> _logger;
    private readonly IRobotRepository _robotRepository;
    private readonly RobotService _robotService;
    private readonly IMapper _mapper;

    public RobotController(ILogger<RobotController> logger,
                           IRobotRepository robotRepository,
                           RobotService robotService,
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
    public async Task<IEnumerable<Robot>> GetAll([FromRoute] bool returnDeletedRecords = false)
    {
        return await _robotRepository.GetAll(returnDeletedRecords);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Robot>> Get(int id)
    {
        var robot = await _robotService.Get(id);
        if (robot == null)
        {
            return NotFound();
        }
        return robot;
    }


    [HttpGet("List")]
    public ActionResult GetRobots()
    {
        var robots = _robotService.GetAllRobots();

        return Ok(robots);
    }

    [HttpPost("Add")]
    public ActionResult AddRobot([FromBody] RobotDto  robotDto)
    {
        _robotService.AddRobot(robotDto);

        return CreatedAtAction(nameof(GetRobots), new {id = robotDto.Id }, robotDto); 
    }

    [HttpPost("Edit")]
    public ActionResult EditRobot([FromBody] RobotDto robotDto)
    {
        _robotService.EditRobot(robotDto.Id, robotDto);
        return NoContent();
    }
}
