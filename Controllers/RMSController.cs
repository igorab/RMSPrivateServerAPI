using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace RMSPrivateServerAPI.Controllers;

using Microsoft.AspNetCore.Mvc;
using RMSPrivateServerAPI.Data;
using RMSPrivateServerAPI.Models;

[Route("api/[controller]")]
[ApiController]
public class RMSController : ControllerBase
{
    private readonly RmsDbContext _context;

    public RMSController(RmsDbContext context)
    {
        _context = context;
    }

    [HttpPost("headers")]
    public async Task<ActionResult<RobotHeader>> CreateRobotHeader(RobotHeader robotHeader)
    {
        _context.RobotHeaders.Add(robotHeader);
        
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(CreateRobotHeader), new { id = robotHeader.Id }, robotHeader);
    }

    [HttpPost("poses")]
    public async Task<ActionResult<Pose>> CreatePose(Pose pose)
    {
        _context.Poses.Add(pose);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(CreatePose), new { id = pose.Id }, pose);
    }

    [HttpPost("state-reports")]
    public async Task<ActionResult<StateReport>> CreateStateReport(StateReport stateReport)
    {
        _context.StateReports.Add(stateReport);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(CreateStateReport), new { id = stateReport.Id }, stateReport);
    }
}

