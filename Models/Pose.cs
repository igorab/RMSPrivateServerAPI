using System.Text.Json.Serialization;

namespace RMSPrivateServerAPI.Models;
        
/// <summary>
/// Представляет положение робота или объекта на плоскости.
/// </summary>
public class Pose
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    [JsonIgnore]
    public int Id { get; set; }
    /// <summary>
    /// Координата X в метрах.
    /// </summary>
    public float X { get; set; }

    /// <summary>
    /// Координата Y в метрах.
    /// </summary>
    public float Y { get; set; }

    /// <summary>
    /// Угол поворота в градусах, от 0 до 360 (измеряется от оси X против часовой стрелки).
    /// </summary>
    public float Heading { get; set; }
}           
