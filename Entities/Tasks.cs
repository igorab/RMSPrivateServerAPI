namespace RMSPrivateServerAPI.Entities;

/// <summary>
/// таблица, которая хранит информацию о задачах в приложении
/// </summary>
public class Tasks
{
    /// <summary>
    /// Unique identifier for each task.
    /// </summary>
    public Guid TaskId { get; set; }
    /// <summary>
    /// Timestamp when the task was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }
    /// <summary>
    /// Timestamp when the task was processed.
    /// </summary>
    public DateTime? ProcessedAt { get; set; }
    /// <summary>
    /// Current status of the task.
    /// </summary>
    public string Status { get; set; }
    /// <summary>
    /// Identifier for the store in WMS.
    /// </summary>
    public string StoreWmsId { get; set; }
    /// <summary>
    /// Identifier for the area in WMS.
    /// </summary>
    public string AreaWmsId { get; set; }
    /// <summary>
    /// Priority level of the task.
    /// </summary>
    public int Priority { get; set; }
}
