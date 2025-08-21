using System.ComponentModel.DataAnnotations.Schema;

namespace RMSPrivateServerAPI.StoreMapPOCO;

/// <summary>
/// Представляет складскую территорию.
/// </summary>
public class Area
{
    /// <summary>
    /// Идентификатор территории.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Ссылка на склад.
    /// </summary>
    public Guid MapId { get; set; }

    /// <summary>
    /// Ссылка на родительскую территорию.
    /// </summary>
    public Guid? ParentId { get; set; }

    /// <summary>
    /// Идентификатор WMS.
    /// </summary>
    public string? WmsID { get; set; }

    /// <summary>
    /// Описание геометрии территории (GeoJSON).
    /// </summary>
    [Column(TypeName = "jsonb")]
    public string? Geometry { get; set; }

    /// <summary>
    /// Наименование территории.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Описание территории (по необходимости).
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
