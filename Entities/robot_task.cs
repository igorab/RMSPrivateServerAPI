using RMSPrivateServerAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#pragma warning disable CS1591, IDE1006

namespace RMSPrivateServerAPI.Entities;

/// <summary>
/// Представляет задачу, назначенную роботу, включая её идентификатор и список операций.
/// </summary>
[Table("robot_task")]
public class robot_task
{
    /// <summary>
    /// Уникальный идентификатор задачи в системе RMS.
    /// </summary>
    [Key]
    public string? task_id { get; set; }

    /// <summary>
    /// Уникальный идентификатор робота, которому назначена задача.
    /// </summary>
    [ForeignKey("robot_id")]
    public string? robot_id { get; set; }
    
    /// <summary>
    /// Человекочитаемое название задачи.
    /// </summary>
    public string? title { get; set; }
    
}
