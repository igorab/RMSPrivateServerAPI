#pragma warning disable CS1591

namespace RMSPrivateServerAPI.StoreMapDto;

public class PointDto
{
    public Guid Id { get; set; }
    public string? IdInWMS { get; set; }
    public float X { get; set; }
    public float Y { get; set; }
    public string? TypeName { get; set; }
}
