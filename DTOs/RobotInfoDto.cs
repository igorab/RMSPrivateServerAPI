using RMSPrivateServerAPI.Enums;
using System.ComponentModel;
#pragma warning disable CS1591
namespace RMSPrivateServerAPI.DTOs;

/// <summary>
/// Представляет информацию о роботе, включая его характеристики и идентификаторы.
/// </summary>
public class RobotInfoDto
{
    /// <summary>
    /// "Уникальный идентификатор робота (PRIMARY KEY)"
    /// </summary>  
    [DefaultValue("00000000-0000-0000-0000-000000000000")]
    public Guid RobotId { get; set; }

    /// <summary>
    /// Аппаратный идентификатор робота
    /// </summary>
    public int RobotHardwareId { get; set; }

    /// <summary>
    /// "Тип робота (например, APR, AMR и т.д.)"
    /// </summary>
    public string? RobotType { get; set; }

    /// <summary>
    /// "Наименование модели робота"
    /// </summary>
    public string? RobotModel { get; set; }

    /// <summary>
    /// "Человекочитаемое имя робота"
    /// </summary>
    public string? RobotName { get; set; }

    /// <summary>
    /// "IP-адрес робота"
    /// </summary>
    public string? IP { get; set; }

    /// <summary>
    /// "Версия программного обеспечения робота (RCS)"
    /// </summary>
    public string? SwVersion { get; set; }

    /// <summary>
    /// "Версия конструктивного исполнения робота"
    /// </summary>
    public string? HwVersion { get; set; }

    /// <summary>
    /// признак - состояние робота
    /// </summary>
    [DefaultValue(0)]
    public RobotState RobotState { get; set; }

}
