using System.ComponentModel.DataAnnotations.Schema;

namespace RMSPrivateServerAPI.StoreMapDto;

/// <summary>
/// Представляет точку на складе.
/// </summary>
public class Point
{
    /// <summary>
    /// Идентификатор точки.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Ссылка на склад.
    /// </summary>
    public Guid MapId { get; set; }

    /// <summary>
    /// Ссылка на территорию.
    /// </summary>
    public Guid AreaId { get; set; }

    /// <summary>
    /// Тип точки.
    /// </summary>
    public Guid TypeId { get; set; }

    /// <summary>
    /// Идентификатор WMS.
    /// </summary>
    public string WmsId { get; set; }

    /// <summary>
    /// X координата точки.
    /// </summary>
    public float X { get; set; }

    /// <summary>
    /// Y координата точки.
    /// </summary>
    public float Y { get; set; }

    /// <summary>
    /// Угол подъезда робота к точке.
    /// </summary>
    public float RotationAngle { get; set; }

    /// <summary>
    /// Метаданные точки.
    /// </summary>
    [Column(TypeName = "jsonb")]
    public string? Metadata { get; set; } // JSON as string

    /// <summary>
    /// Описание точки (по необходимости).
    /// </summary>
    public string? Desc { get; set; }

    /// <summary>
    /// Дата/время создания записи.
    /// </summary>
    public DateTime CreatedOn { get; set; }

    /// <summary>
    /// Дата/время изменения записи.
    /// </summary>
    public DateTime ModifiedOn { get; set; }

    /// <summary>
    /// ID пользователя, который создал запись.
    /// </summary>
    public Guid CreatedBy { get; set; }

    /// <summary>
    /// ID пользователя, который изменил запись.
    /// </summary>
    public Guid ModifiedBy { get; set; }
}
