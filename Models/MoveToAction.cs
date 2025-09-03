using RMSPrivateServerAPI.Enums;

namespace RMSPrivateServerAPI.Models;

/// <summary>
/// Движение в заданную позицию
/// </summary>
public class MoveToAction : RobotAction
{
    /// <summary>
    /// MoveTo
    /// </summary>
    public MoveToAction()
    {
        ActionTypeId = ActionType.moveTo;
        ActionName = nameof(ActionType.moveTo);
    }

    /// <summary>
    /// Положение робота или объекта на плоскости
    /// </summary>
    public Pose? Pose { get; set; }
}



