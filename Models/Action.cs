namespace RMSPrivateServerAPI.Models
{
    /// <summary>
    /// Классы для различных типов действий
    /// </summary>
    // Models/Action.cs
    public abstract class RobotAction
    {
        public string ActionType { get; set; }
    }

    /// <summary>
    /// Движение к заданной позиции.
    /// </summary>
    //Models/MoveToAction.cs
    public class MoveToAction : RobotAction
    {
        public MoveToAction()
        {
            ActionType = "moveTo";
        }

        public Pose Pose { get; set; }
    }

    /// <summary>
    /// Прямолинейное движение.
    /// </summary>
    // Models/MoveLinearAction.cs
    public class MoveLinearAction : RobotAction
    {
        public MoveLinearAction()
        {
            ActionType = "moveLinear";
        }

        public string Direction { get; set; } // forward или backward
        public float Distance { get; set; } // Расстояние в метрах
    }



}
