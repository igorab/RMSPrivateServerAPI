using Microsoft.EntityFrameworkCore;
using RMSPrivateServerAPI.Data;
using RMSPrivateServerAPI.StoreMapDto;
using System.Collections.Generic;
using System.Linq;
#pragma warning disable CS1591

namespace RMSPrivateServerAPI.Services;
public class PointService
{
    private readonly WmsDbContext _context;

    public PointService(WmsDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="areaId"></param>
    /// <returns></returns>
    public async Task<List<PathDto>> GetPathElementsWithTypesAndPathsAsync(Guid areaId)
    {        
        var query = from path in _context.Paths
                    join pathElement in _context.PathElements on path.Id equals pathElement.PathId
                    join pathElementType in _context.PathElementTypes on pathElement.TypeId equals pathElementType.Id
                    where pathElement.AreaId == areaId // Add the condition here
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
                        IdInWMS = point.IdInWMS,
                        X = point.X,
                        Y = point.Y,
                        TypeName = pointType.Name
                    };

        var res = await query.ToListAsync();
        return res;
    }
}
