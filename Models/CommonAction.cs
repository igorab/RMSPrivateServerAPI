using RMSPrivateServerAPI.Enums;
using System.Text.Json.Serialization;
namespace RMSPrivateServerAPI.Models;
#pragma warning disable CS1591

public class CommonAction : RobotAction
{
    // Основной класс без дополнительных параметров
}

public class StopAction : RobotAction
{
 
}

public class MoveToAction : RobotAction
{    
    public Pose? Pose { get; set; }
}

public class MoveLinearAction : RobotAction
{    
    public Direction Direction { get; set; } // forward/backward
    public double Distance { get; set; }
}

public class TurnAction : RobotAction
{    
    public TurnDirection Direction { get; set; } // left/right
    public double Angle { get; set; }
}

public class MoveArcAction : RobotAction
{    
    public ArcDirection Direction { get; set; } // forward-left/forward-right/backward-left/backward-right
    public double Radius { get; set; }
    public double Angle { get; set; }
}



