namespace RMSPrivateServerAPI.StoreMapDto;

/// <summary>
/// Представляет элемент маршрута.
/// </summary>
public class PathElement
{
    /// <summary>
    /// Идентификатор элемента маршрута.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Ссылка на маршрут.
    /// </summary>
    public Guid PathId { get; set; }

    /// <summary>
    /// Номер п/п в маршруте.
    /// </summary>
    public int Order { get; set; }

    /// <summary>
    /// Ссылка на территорию.
    /// </summary>
    public Guid AreaId { get; set; }

    /// <summary>
    /// Ссылка на точку (Point) начала маршрута.
    /// </summary>
    public Guid StartId { get; set; }

    /// <summary>
    /// Ссылка на точку (Point) окончания маршрута.
    /// </summary>
    public Guid FinishId { get; set; }

    /// <summary>
    /// Тип (схема) элемента маршрута.
    /// </summary>
    public Guid TypeId { get; set; }

    /// <summary>
    /// Описание геометрии элемента маршрута.
    /// </summary>
    public string? Geometry { get; set; } // Формат описания необходимо уточнить
}
