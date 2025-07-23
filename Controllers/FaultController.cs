using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RMSPrivateServerAPI.Data;
using RMSPrivateServerAPI.Entities;

namespace RMSPrivateServerAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FaultController : ControllerBase
{
    private readonly ApplicationDbContext? _context;

    /// <summary>
    /// Сообщение о возникшей неисправности/ошибке
    /// </summary>
    /// <param name="faults"></param>
    /// <returns></returns>
    [HttpPost("raise")]
    public IActionResult RaiseFault([FromBody] List<FaultInfo> faults)
    {
        // Логирование аварийных ситуаций
        foreach (var fault in faults)
        {
            _context.Faults.Add(fault);
        }
        return Ok();
    }
}
