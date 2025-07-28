using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

using RMSPrivateServerAPI.DTOs;
using RMSPrivateServerAPI.Entities;
using RMSPrivateServerAPI.Interfaces;

#pragma warning disable CS1591
namespace RMSPrivateServerAPI.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v1.0/Common/[controller]/")]
//[Description("Ведение справочника роботов")]
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
    /// Получить список роботов из базы данных
    /// </summary>
    /// <param name="returnDeletedRecords">в т.ч. удаленные</param>     
    [HttpGet("List")]
    public async Task<IEnumerable<robotinfo>> GetAll([FromRoute] bool returnDeletedRecords = false)
    {        
        return await _robotRepository.GetAll(returnDeletedRecords);
    }

    /// <summary>
    /// Получить параметры робота
    /// </summary>
    /// <param name="id">Id робота</param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<robotinfo>> Get(string id)
    {
        var robot = await _robotService.Get(id);
        if (robot == null)
        {
            return NotFound();
        }
        return robot;
    }

    /// <summary>
    /// Добавить. Робот отправляет свои характеристики.
    /// </summary>
    /// <param name="robotAsDto"></param>
    /// <returns></returns>
    [HttpPost("Register")]
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

            var location = $"https://localhost/RobotInfo/{insertedRobotDto.RobotId}"; ;

            return Created(location, insertedRobotDto);

        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Редактировать
    /// </summary>
    /// <param name="robot"></param>
    /// <returns></returns>
    [HttpPut("Edit")]
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

    /// <summary>
    /// Удалить. 
    /// </summary>
    /// <param name="id"> Id робота </param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
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
}
