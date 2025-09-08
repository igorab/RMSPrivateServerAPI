using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#pragma warning disable CS1591, IDE1006

namespace RMSPrivateServerAPI.Entities;

/// <summary>
/// Представляет задачу, назначенную роботу, включая её идентификатор и список операций.
/// </summary>
[Table("RobotTask")]
public class robot_task
{
    /// <summary>
    /// Уникальный идентификатор задачи в системе RMS.
    /// </summary>
    [Key]
    public Guid TaskId { get; set; }

    /// <summary>
    /// Уникальный идентификатор робота, которому назначена задача.
    /// </summary>
    //[ForeignKey("RobotId")]
    public Guid RobotId { get; set; }
    
    /// <summary>
    /// Человекочитаемое название задачи.
    /// </summary>
    public string? Title { get; set; }
    
}
