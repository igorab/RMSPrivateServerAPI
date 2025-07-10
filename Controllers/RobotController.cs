using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RMSPrivateServerAPI.DTOs;
using RMSPrivateServerAPI.Models;
using RMSPrivateServerAPI.Services;

namespace RMSPrivateServerAPI.Controllers;

[ApiController]
[Route("api/Robot/v1.0/robot")]
public class RobotController : ControllerBase
{
    private readonly RobotService _robotService;

    public RobotController(RobotService robotService)
    {
        _robotService = robotService;
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
