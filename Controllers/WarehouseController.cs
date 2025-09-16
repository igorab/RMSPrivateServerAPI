using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RMSPrivateServerAPI.Data;
using RMSPrivateServerAPI.DTOs;
using RMSPrivateServerAPI.StoreMapPOCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
#pragma warning disable CS1591
namespace RMSPrivateServerAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WarehouseController : ControllerBase
{
    private readonly WmsDbContext _context;

    public WarehouseController(WmsDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Копирование задачи
    /// </summary>
    /// <param name="taskId">Task Guid</param>
    /// <returns></returns>
    [HttpPost("CopyTask/{taskId}")]    
    public async Task<IActionResult> CopyTask(Guid taskId)
    {
        var existingTask = await _context.Tasks.FirstOrDefaultAsync(t => t.TaskId == taskId);
        if (existingTask == null)
        {
            return NotFound("Task not found.");
        }

        // Копирование задачи
        var newTask = new TasksDto
        {
            TaskId = Guid.NewGuid(), // Генерация нового уникального идентификатора
            CreatedAt = DateTime.UtcNow,
            Status = "Received", // Установка статуса
            StoreWmsId = existingTask.StoreWmsId,
            AreaWmsId = existingTask.AreaWmsId,
            Priority = existingTask.Priority
        };

        _context.Tasks.Add(newTask);
        await _context.SaveChangesAsync();

        
        // Копирование действий
        foreach (var action in _context.TaskActions.Where(ta => ta.TaskId == existingTask.TaskId))
        {
            var newAction = new TaskActionsDto
            {
                TaskId = newTask.TaskId, // Связываем новое действие с новой задачей
                CreatedAt = DateTime.UtcNow,
                Status = action.Status,
                Location = action.Location,
                ActionType = action.ActionType,
                ActionOrder = action.ActionOrder
            };

            _context.TaskActions.Add(newAction);
        }

        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(CopyTask), new { taskId = newTask.TaskId }, newTask);
    }


    [ApiExplorerSettings(IgnoreApi = true)]
    // GET: api/warehouse/stores
    [HttpGet("stores")]
    public async Task<ActionResult<IEnumerable<Store>>> GetStores()
    {
        return await _context.Stores.ToListAsync();
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    // GET: api/warehouse/areas
    [HttpGet("areas")]
    public async Task<ActionResult<IEnumerable<Area>>> GetAreas()
    {
        return await _context.Areas.ToListAsync();
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    // GET: api/warehouse/zones
    [HttpGet("zones")]
    public async Task<ActionResult<IEnumerable<Zone>>> GetZones()
    {
        return await _context.Zones.ToListAsync();
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    // GET: api/warehouse/points
    [HttpGet("points")]
    public async Task<ActionResult<IEnumerable<Point>>> GetPoints()
    {
        return await _context.Points.ToListAsync();
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    // GET: api/warehouse/paths
    [HttpGet("paths")]
    public async Task<ActionResult<IEnumerable<StoreMapPOCO.Path>>> GetPaths()
    {
        return await _context.Paths.ToListAsync();
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    // GET: api/warehouse/pathElements
    [HttpGet("pathElements")]
    public async Task<ActionResult<IEnumerable<PathElement>>> GetPathElements()
    {
        return await _context.PathElements.ToListAsync();
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    // GET: api/warehouse/zoneTypes
    [HttpGet("zoneTypes")]
    public async Task<ActionResult<IEnumerable<ZoneType>>> GetZoneTypes()
    {
        return await _context.ZoneTypes.ToListAsync();
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    // GET: api/warehouse/pointTypes
    [HttpGet("pointTypes")]
    public async Task<ActionResult<IEnumerable<PointType>>> GetPointTypes()
    {
        return await _context.PointTypes.ToListAsync();
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    // GET: api/warehouse/pathElementTypes
    [HttpGet("pathElementTypes")]
    public async Task<ActionResult<IEnumerable<PathElementType>>> GetPathElementTypes()
    {
        return await _context.PathElementTypes.ToListAsync();
    }
}
