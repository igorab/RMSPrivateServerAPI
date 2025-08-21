#pragma warning disable CS1591

namespace RMSPrivateServerAPI.StoreMapDto;

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
