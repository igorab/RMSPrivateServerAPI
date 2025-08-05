namespace RMSPrivateServerAPI.DTOs;

using System;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// Класс для работы с таблицей TaskActions
/// </summary>
public class TaskActionsDto
{
    /// <summary>
    /// Уникальный идентификатор действия
    /// </summary>
    [Key]
    public int Id { get; set; }
    /// <summary>
    /// Идентификатор связанной задачи
    /// </summary>
    public Guid TaskId { get; set; }
    /// <summary>
    /// Дата и время создания действия
    /// </summary>
    public DateTime CreatedAt { get; set; }
    /// <summary>
    /// Текущий статус действия
    /// </summary>
    public string? Status { get; set; }
    /// <summary>
    /// Местоположение, где выполнено действие
    /// </summary>
    public string? Location { get; set; }
    /// <summary>
    /// Тип действия
    /// </summary>
    public int ActionType { get; set; }
    /// <summary>
    /// Порядок действия
    /// </summary>
    public int ActionOrder { get; set; } 
    /// <summary>
    /// Навигационное свойство для связи с задачей (если необходимо)
    /// </summary>
    //public virtual Task Task { get; set; } // Связь с классом Task
}
