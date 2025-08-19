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
    public StopAction()
    {
        ActionTypeId = ActionType.stop;
        ActionName = nameof(ActionType.stop);
    }

}



