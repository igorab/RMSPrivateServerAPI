using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RMSPrivateServerAPI.Data;
using RMSPrivateServerAPI.Entities;
#pragma warning disable CS1591
namespace RMSPrivateServerAPI.Controllers;


[ApiController]
[Route("api/[controller]")]
public class FaultController : ControllerBase
{
    private readonly WmsDbContext? _context;

    public FaultController(WmsDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Сообщение о возникшей неисправности/ошибке
    /// </summary>
    /// <param name="faults"></param>
    /// <returns></returns>
    [HttpPost("raise")]
    public IActionResult RaiseFault([FromBody] List<FaultInfoDto> faults)
    {
        // Логирование аварийных ситуаций
        foreach (var fault in faults)
        {
            _context?.Faults.Add(fault);
            _context?.SaveChanges();
        }
        return Ok();
    }
}
