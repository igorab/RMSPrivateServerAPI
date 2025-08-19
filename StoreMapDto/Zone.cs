using System.ComponentModel.DataAnnotations.Schema;
namespace RMSPrivateServerAPI.StoreMapDto;

/// <summary>
/// Представляет зону на складе.
/// </summary>
public class Zone
{
    /// <summary>
    /// Идентификатор зоны.
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
    /// Идентификатор WMS.
    /// </summary>
    public string? WmsId { get; set; }

    /// <summary>
    /// Тип зоны.
    /// </summary>
    public Guid TypeId { get; set; }

    /// <summary>
    /// Описание геометрии зоны (GeoJSON).
    /// </summary>
    [Column(TypeName = "jsonb")]
    public string? Geometry { get; set; } // GeoJSON as string

    /// <summary>
    /// Описание зоны (по необходимости).
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
