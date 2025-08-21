using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RMSPrivateServerAPI.Data;
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

    // GET: api/warehouse/stores
    [HttpGet("stores")]
    public async Task<ActionResult<IEnumerable<Store>>> GetStores()
    {
        return await _context.Stores.ToListAsync();
    }

    // GET: api/warehouse/areas
    [HttpGet("areas")]
    public async Task<ActionResult<IEnumerable<Area>>> GetAreas()
    {
        return await _context.Areas.ToListAsync();
    }

    // GET: api/warehouse/zones
    [HttpGet("zones")]
    public async Task<ActionResult<IEnumerable<Zone>>> GetZones()
    {
        return await _context.Zones.ToListAsync();
    }

    // GET: api/warehouse/points
    [HttpGet("points")]
    public async Task<ActionResult<IEnumerable<Point>>> GetPoints()
    {
        return await _context.Points.ToListAsync();
    }

    // GET: api/warehouse/paths
    [HttpGet("paths")]
    public async Task<ActionResult<IEnumerable<StoreMapPOCO.Path>>> GetPaths()
    {
        return await _context.Paths.ToListAsync();
    }

    // GET: api/warehouse/pathElements
    [HttpGet("pathElements")]
    public async Task<ActionResult<IEnumerable<PathElement>>> GetPathElements()
    {
        return await _context.PathElements.ToListAsync();
    }

    // GET: api/warehouse/zoneTypes
    [HttpGet("zoneTypes")]
    public async Task<ActionResult<IEnumerable<ZoneType>>> GetZoneTypes()
    {
        return await _context.ZoneTypes.ToListAsync();
    }

    // GET: api/warehouse/pointTypes
    [HttpGet("pointTypes")]
    public async Task<ActionResult<IEnumerable<PointType>>> GetPointTypes()
    {
        return await _context.PointTypes.ToListAsync();
    }

    // GET: api/warehouse/pathElementTypes
    [HttpGet("pathElementTypes")]
    public async Task<ActionResult<IEnumerable<PathElementType>>> GetPathElementTypes()
    {
        return await _context.PathElementTypes.ToListAsync();
    }
}
