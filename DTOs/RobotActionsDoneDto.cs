using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace RMSPrivateServerAPI.DTOs;

/// <summary>
///  Завершённая операция
///  RobotActionsDoneDto
/// </summary>
[Table("RobotActions")]
public class RobotActionsDoneDto
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
}
