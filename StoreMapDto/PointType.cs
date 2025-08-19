namespace RMSPrivateServerAPI.StoreMapDto;

/// <summary>
/// Представляет тип точки.
/// </summary>
public class PointType
{
    /// <summary>
    /// Идентификатор типа точки.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Наименование типа точки.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Иконка с изображением типа точки.
    /// </summary>
    public byte[]? Icon { get; set; } // Image as byte

    /// <summary>
    /// Описание типа точки.
    /// </summary>
    public string? Desc { get; set; }
}
