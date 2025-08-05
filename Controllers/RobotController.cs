using Asp.Versioning;
using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Mvc;

using RMSPrivateServerAPI.DTOs;
using RMSPrivateServerAPI.Entities;
using RMSPrivateServerAPI.Interfaces;
using System;

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
    public async Task<IEnumerable<robot_info>> GetAll([FromRoute] bool returnDeletedRecords = false)
    {        
        return await _robotRepository.GetAll(returnDeletedRecords);
    }

    /// <summary>
    /// Получить параметры робота
    /// </summary>
    /// <param name="id">Id робота</param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<robot_info>> Get(Guid id)
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
    public async Task<ActionResult<robot_info>> RegisterRobot([FromBody] RobotInfoDto robotAsDto)
    {
        try
        {
            if (robotAsDto == null)
            {
                return BadRequest("No Robot was provided");
            }

            if (robotAsDto.RobotId == Guid.Empty)
            {
                robotAsDto.RobotId = Guid.NewGuid();
            }
            else
            {
                var robot_exist = await _robotService.Get(robotAsDto.RobotId);
                if (robot_exist != null)
                {
                    return Ok("Робот с таким RobotId уже был зарегистрирован.");
                }
            }

            robot_info robotToInsert = _mapper.Map<robot_info>(robotAsDto);

            robot_info insertedRobot = await _robotService.Insert(robotToInsert);

            RobotInfoDto insertedRobotDto = _mapper.Map<RobotInfoDto>(insertedRobot);
                        
            var res = CreatedAtAction(nameof(RegisterRobot), new
                            {
                                message = "Робот успешно зарегистрирован в системе (добавлен в БД)",
                                insertedRobotDto.RobotId
                            });

            return res;

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
    public async Task<IActionResult> Put([FromBody] robot_info robot)
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
    public async Task<IActionResult> Delete(Guid id)
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
