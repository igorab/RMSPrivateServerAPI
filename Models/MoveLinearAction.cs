using RMSPrivateServerAPI.Enums;
namespace RMSPrivateServerAPI.Models;

/// <summary>
/// Прямолинейное движение
/// </summary>
public class MoveLinearAction : RobotAction
{
    /// <summary>
    /// Направление движения (вперёд или назад)
    /// </summary>
    public Direction Direction { get; set; } // forward/backward
    /// <summary>
    /// Расстояние в метрах, на которое робот должен переместиться прямолинейно
    /// </summary>
    public double Distance { get; set; }
}



