using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMSPrivateServerAPI.Entities;
#pragma warning disable CS1591, IDE1006

/// <summary>
///  Завершённая операция
/// </summary>
[Table("RobotActions")]
public class RobotActionsDone
{
    /// <summary>
    /// Key
    /// </summary>
    [Key]
    public Guid ActionId { get; set; }
    /// <summary>
    /// Идентификатор задачи в RMS
    /// </summary>
    public Guid TaskId { get; set; }
    /// <summary>
    /// Robot id Guid
    /// </summary>
    public Guid RobotId { get; set; }
    /// <summary>
    /// Индекс завершённой операции в текущем списке подзадач
    /// </summary>
    public int ActionIndex { get; set; }
    /// <summary>
    /// Результат выполнения текущей операции
    /// </summary>
    [Column("Title")]
    public string? Result { get; set; }

    /// <summary>
    /// Причина провала (поле присутствует, если результат выполнения операции - failed)
    /// </summary>    
    public string? Reason { get; set; }

    public int  ActionType { get; set; }

    public float? Pose_X { get; set; }

    public float? Pose_Y { get; set; }

    public float? Heading { get; set; }

    public int? Direction { get; set; }

    public float? Distance { get; set; }

    public float? Angle { get; set; }

    public float? Radius { get; set; }
}
