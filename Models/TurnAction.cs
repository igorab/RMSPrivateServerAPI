using RMSPrivateServerAPI.Enums;
namespace RMSPrivateServerAPI.Models;

/// <summary>
///  Поворот на месте
/// </summary>
public class TurnAction : RobotAction
{
    /// <summary>
    ///  конструктор
    /// </summary>
    public TurnAction()
    {
        ActionTypeId = ActionType.turn;
        ActionName = nameof(ActionType.turn);
    }
    /// <summary>
    /// Направление движения(влево - против часовой стрелки, вправо - по часовой)
    /// </summary>
    public TurnDirection Direction { get; set; } // left/right

    /// <summary>
    /// Угол в градусах, на который робот должен повернуть
    /// </summary>
    public double Angle { get; set; }
}



