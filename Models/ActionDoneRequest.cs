using System.Text.Json.Serialization;

namespace RMSPrivateServerAPI.Models;

/// <summary>
/// Завершённая операция
/// </summary>
public class ActionDoneRequest
{
    [JsonPropertyName("taskID")]
    public Guid TaskId { get; set; }

    [JsonPropertyName("actionIndex")]
    public int ActionIndex { get; set; }

    [JsonPropertyName("result")]
    public string Result { get; set; } // "success" или "failed"

    [JsonPropertyName("Reason")]
    public string Reason { get; set; } 
}
