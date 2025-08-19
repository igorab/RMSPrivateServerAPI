namespace RMSPrivateServerAPI.StoreMapDto;

/// <summary>
/// Представляет тип зоны.
/// </summary>
public class ZoneType
{
    /// <summary>
    /// Идентификатор типа зоны.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Наименование типа зоны.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// HEX значение цвета зоны.
    /// </summary>
    public string? Color { get; set; }

    /// <summary>
    /// Описание типа зоны.
    /// </summary>
    public string? Desc { get; set; }
}
