#pragma warning disable CS1591
namespace RMSPrivateServerAPI.Controllers;

using Microsoft.AspNetCore.Mvc;
using RMSPrivateServerAPI.Data;
using RMSPrivateServerAPI.Models;

/// <summary>
/// Получение State Report от RCS 
/// </summary>
[Route("api/[controller]")]
[ApiController]
[ApiExplorerSettings(IgnoreApi = true)]
public class RMSController : ControllerBase
{
    private readonly RmsDbContext _context;

    /// <summary>
    /// RMS - RCS взаимодействие
    /// </summary>
    /// <param name="context"></param>
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

