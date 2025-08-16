namespace RMSPrivateServerAPI.Models;

/// <summary>
/// Движение в заданную позицию
/// </summary>
public class MoveToAction : RobotAction
{
    /// <summary>
    /// Положение робота или объекта на плоскости
    /// </summary>
    public Pose? Pose { get; set; }
}



