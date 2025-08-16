namespace RMSPrivateServerAPI.Enums;
#pragma warning disable CS1591

/// <summary>
/// Операции робота
/// </summary>
public enum ActionType
{
    stop, 
    wait, 
    moveTo, 
    moveLinear, 
    turn, 
    moveArc, 
    load, 
    unload, 
    loadInPlace, 
    unloadInPlace, 
    goParking, 
    goCharge
}

