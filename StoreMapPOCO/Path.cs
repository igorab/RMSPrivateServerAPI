namespace RMSPrivateServerAPI.StoreMapPOCO;

/// <summary>
/// Представляет маршрут на складе.
/// </summary>
public class Path
{
    /// <summary>
    /// Идентификатор маршрута.
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
    /// Наименование маршрута.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Описание маршрута (по необходимости).
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
