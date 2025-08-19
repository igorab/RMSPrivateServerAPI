using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RMSPrivateServerAPI.Data;
#pragma warning disable CS1591

namespace RMSPrivateServerAPI.Services;
public class PointService
{
    private readonly WmsDbContext _context;

    public PointService(WmsDbContext context)
    {
        _context = context;
    }

    public async Task<List<PathDto>> GetPathElementsWithTypesAndPathsAsync()
    {        
        var query = from path in _context.Paths
                    join pathElement in _context.PathElements on path.Id equals pathElement.PathId
                    join pathElementType in _context.PathElementTypes on pathElement.TypeId equals pathElementType.Id
                    select new PathDto
                    {
                        PathId = path.Id,
                        PathName = path.Name,
                        PathDesc = path.Desc,
                        CreatedOn = path.CreatedOn,
                        ModifiedOn = path.ModifiedOn,
                        PathElementId = pathElement.Id,
                        Order = pathElement.Order,
                        AreaId = pathElement.AreaId,
                        StartId = pathElement.StartId,
                        FinishId = pathElement.FinishId,
                        TypeId = pathElement.TypeId,
                        Geometry = pathElement.Geometry,
                        ElementTypeDesc = pathElementType.Desc,
                        ElementTypeScheme = pathElementType.Scheme
                    };

        var result = await query.ToListAsync();

        return result;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public async Task<List<PointDto>> GetPointsWithTypesAsync()
    {
        var query = from point in _context.Points
                    join pointType in _context.PointTypes
                    on point.TypeId equals pointType.Id
                    select new PointDto
                    {
                        Id = point.Id,
                        WmsId = point.WmsId,
                        X = point.X,
                        Y = point.Y,
                        TypeName = pointType.Name
                    };

        var res = await query.ToListAsync();
        return res;
    }
}

public class PathDto
{
    public Guid PathId { get; set; }
    public string? PathName { get; set; }
    public string? PathDesc { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime ModifiedOn { get; set; }
    public Guid PathElementId { get; set; }
    public int Order { get; set; }
    public Guid AreaId { get; set; }
    public Guid StartId { get; set; }
    public Guid FinishId { get; set; }
    public Guid TypeId { get; set; }
    public string? Geometry { get; set; }
    public string? ElementTypeDesc { get; set; }
    public string? ElementTypeScheme { get; set; }
}


public class PointDto
{
    public Guid Id { get; set; }
    public string? WmsId { get; set; }
    public float X { get; set; }
    public float Y { get; set; }
    public string? TypeName { get; set; }
}
