using System.Runtime.InteropServices;
#pragma warning disable CS1591
namespace RMSPrivateServerAPI.DTOs;

public class RobotActionsDto
{
    public string? ActionId { get; set; }
    public string? ActionName { get; set; }
    public int ActionType { get; set; }
    public float  ActionValue { get; set; }
}
