using System.Text.Json.Serialization;
#pragma warning disable CS1591

namespace RMSPrivateServerAPI.Models;

/// <summary>
/// Завершённая операция
/// </summary>
public class ActionDoneRequest
{
    [JsonPropertyName("taskId")]
    public Guid TaskId { get; set; }

    [JsonPropertyName("actionIndex")]
    public int ActionIndex { get; set; }

    [JsonPropertyName("result")]
    public string? Result { get; set; } // "success" или "failed"

    [JsonPropertyName("reason")]
    public string? Reason { get; set; } 
}
