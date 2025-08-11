using RMSPrivateServerAPI.DTOs;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RMSPrivateServerAPI.Models.Lib;

/// <summary>
/// конвертер для полиморфной сериализации типа RobotAction
/// </summary>
public class RobotActionConverter : JsonConverter<RobotAction>
{
    public override RobotAction Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        // Реализовать логику десериализации, если будет необходимо
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, RobotAction value, JsonSerializerOptions options)
    {
        switch (value)
        {
            case CommonAction commonAction:
                JsonSerializer.Serialize(writer, commonAction, options);
                break;
            case StopAction stopAction:
                JsonSerializer.Serialize(writer, stopAction, options);
                break;
            case MoveToAction moveToAction:
                JsonSerializer.Serialize(writer, moveToAction, options);
                break;
            case MoveLinearAction moveLinearAction:
                JsonSerializer.Serialize(writer, moveLinearAction, options);
                break;
            case TurnAction turnAction:
                JsonSerializer.Serialize(writer, turnAction, options);
                break;
            case MoveArcAction moveArcAction:
                JsonSerializer.Serialize(writer, moveArcAction, options);
                break;
            default:
                throw new NotSupportedException($"Type {value.GetType()} is not supported.");
        }
    }
}

