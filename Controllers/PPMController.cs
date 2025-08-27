using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RMSPrivateServerAPI.DTOs;
using RMSPrivateServerAPI.Entities;
using RMSPrivateServerAPI.Interfaces;

#pragma warning disable CS1591
namespace RMSPrivateServerAPI.Controllers;

[ApiController]
[Route("api/v1/PPM")]
[ApiExplorerSettings(IgnoreApi = true)]
public class PPMController : ControllerBase
{
    private readonly ILogger<PPMController> _logger;
    private readonly IPPMRepository _ppmRepository;
    private readonly IPPMService _ppmService;
    private readonly IMapper _mapper;
   
    public PPMController(ILogger<PPMController> logger, 
                         IPPMRepository ppmRepository,           
                         IPPMService ppmService,
                         IMapper mapper)
    {
        _logger = logger;
        _ppmRepository = ppmRepository;
        _ppmService = ppmService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<ppm_task>> GetAll([FromRoute] bool returnDeletedRecords = false ) 
    {
        return await _ppmRepository.GetAll();
    }

    [HttpGet("List")]
    public ActionResult<IEnumerable<PPMTaskDto>> GetPPMTasks(Guid robotId)
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
        _ppmService.EditPPMTask(Guid.Empty, ppmTaskDto);

        return NoContent();
    }

}
