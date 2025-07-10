using Microsoft.AspNetCore.Mvc;
using RMSPrivateServerAPI.DTOs;
using RMSPrivateServerAPI.Services;

namespace RMSPrivateServerAPI.Controllers;

[ApiController]
[Route("api/Robot/v1.0/PPM")]
public class PPMController : ControllerBase
{
    private readonly PPMService _ppmService;

    public PPMController(PPMService ppmService)
    {
        _ppmService = ppmService;
    }

    [HttpGet("List")]
    public ActionResult<IEnumerable<PPMTaskDto>> GetPPMTasks(int robotId)
    {
        var tasks = _ppmService.GetTasksByRobotId(robotId);

        return Ok(tasks);
    }

    [HttpPost("Add")]
    public ActionResult AddPPMTask([FromBody] PPMTaskDto ppmTaskDto)
    {
        _ppmService.AddPPMTask(ppmTaskDto);

        return CreatedAtAction(nameof(GetPPMTasks), new { robotId = ppmTaskDto.RobotId }, ppmTaskDto);
    }

    [HttpPost("Edit")]
    public ActionResult EditPPMTask([FromBody]  PPMTaskDto ppmTaskDto)
    {
        _ppmService.EditPPMTask(0, ppmTaskDto);

        return NoContent();
    }

}
