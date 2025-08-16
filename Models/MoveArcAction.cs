using RMSPrivateServerAPI.Enums;
namespace RMSPrivateServerAPI.Models;

/// <summary>
/// Движение по дуге
/// </summary>
public class MoveArcAction : RobotAction
{
    /// <summary>
    /// Направление движения
    /// </summary>
    public ArcDirection Direction { get; set; } // forward-left/forward-right/backward-left/backward-right
    /// <summary>
    /// Радиус поворота, м.
    /// </summary>
    public double Radius { get; set; }
    /// <summary>
    /// Угол дуги в градусах, на который робот должен повернуть
    /// </summary>
    public double Angle { get; set; }
}



