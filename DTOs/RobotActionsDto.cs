using System.Runtime.InteropServices;
#pragma warning disable CS1591
namespace RMSPrivateServerAPI.DTOs;

public class RobotActionsDto
{
    public string? ActionId { get; set; }
    public string? Title { get; set; }
    public int ActionType { get; set; }
    public float? Pose_X { get; set; }
    public float? Pose_Y { get; set; }
    public float? Heading { get; set; }
    public int? Direction { get; set; }
    public float? Distance { get; set; }
    public float? Angle { get; set; }
    public float? Radius { get; set; }
}
