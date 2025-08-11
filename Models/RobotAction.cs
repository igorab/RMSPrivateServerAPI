using RMSPrivateServerAPI.Enums;
using RMSPrivateServerAPI.Interfaces;
using System.Text.Json.Serialization;
#pragma warning disable CS1591

namespace RMSPrivateServerAPI.Models;


public abstract class RobotAction 
{
    [JsonIgnore]
    public ActionType ActionTypeId { get; set; }

    [JsonPropertyName("actionType")]
    public string? ActionName { get; set; }
}
