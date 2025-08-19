using System.ComponentModel.DataAnnotations.Schema;

namespace RMSPrivateServerAPI.StoreMapDto;

/// <summary>
/// Представляет тип элемента маршрута.
/// </summary>
public class PathElementType
{
    /// <summary>
    /// Идентификатор типа элемента маршрута.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Описание схемы в виде GeoJSON.
    /// </summary>
    [Column(TypeName = "jsonb")]
    public string? Scheme { get; set; } // GeoJSON as string

    /// <summary>
    /// Описание типа элемента маршрута.
    /// </summary>
    public string? Desc { get; set; }
}
