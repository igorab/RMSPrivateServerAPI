using RMSPrivateServerAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMSPrivateServerAPI.Entities;

/// <summary>
/// Представляет задачу, назначенную роботу, включая её идентификатор и список операций.
/// </summary>
public class robot_task
{
    /// <summary>
    /// Уникальный идентификатор задачи в системе RMS.
    /// </summary>
    [Key]
    public string task_id { get; set; }

    /// <summary>
    /// Уникальный идентификатор робота, которому назначена задача.
    /// </summary>
    [ForeignKey("robotid")]
    public string robot_id { get; set; }
    
    /// <summary>
    /// Человекочитаемое название задачи.
    /// </summary>
    public string title { get; set; }

    /// <summary>
    /// Список операций, которые должны быть выполнены в рамках этой задачи.
    /// </summary>
    public List<RobotAction>? actions { get; set; }
}
