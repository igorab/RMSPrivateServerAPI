namespace RMSPrivateServerAPI.StoreMapDto;

/// <summary>
/// Представляет склад с его атрибутами.
/// </summary>
public class Store
{
    /// <summary>
    /// Идентификатор склада.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Идентификатор WMS.
    /// </summary>
    public string? WmsId { get; set; }

    /// <summary>
    /// Бинарное содержимое файла изображения карты.
    /// </summary>
    public byte[]? ContentImage { get; set; }

    /// <summary>
    /// Оригинальное имя файла изображения.
    /// </summary>
    public string? NameImage { get; set; }

    /// <summary>
    /// Бинарное содержимое файла навигации.
    /// </summary>
    public byte[]? ContentNavi { get; set; }

    /// <summary>
    /// Оригинальное имя файла навигации.
    /// </summary>
    public string? NameNavi { get; set; }

    /// <summary>
    /// Наименование склада.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Описание склада (по необходимости).
    /// </summary>
    public string? Desc { get; set; }

    /// <summary>
    /// Дата/время создания записи.
    /// </summary>
    public DateTime? CreatedOn { get; set; }

    /// <summary>
    /// Дата/время изменения записи.
    /// </summary>
    public DateTime? ModifiedOn { get; set; }

    /// <summary>
    /// ID пользователя, который создал запись.
    /// </summary>
    public Guid? CreatedBy { get; set; }

    /// <summary>
    /// ID пользователя, который изменил запись.
    /// </summary>
    public Guid? ModifiedBy { get; set; }
}
