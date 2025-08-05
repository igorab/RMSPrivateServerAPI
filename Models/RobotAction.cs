using RMSPrivateServerAPI.Enums;
using System.Text.Json.Serialization;
#pragma warning disable CS1591

namespace RMSPrivateServerAPI.Models;

public class RobotAction
{
    [JsonIgnore]
    public ActionType ActionType { get; set; }

    [JsonPropertyName("actionType")]
    public string? ActionName { get; set; }
}
