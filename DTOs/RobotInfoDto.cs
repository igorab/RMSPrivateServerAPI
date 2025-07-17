using System.ComponentModel;

namespace RMSPrivateServerAPI.DTOs;

/// <summary>
/// Представляет информацию о роботе, включая его характеристики и идентификаторы.
/// </summary>
public class RobotInfoDto
{        
    /// <summary>
    /// "Уникальный идентификатор робота (PRIMARY KEY)"
    /// </summary>
    public int RobotId { get; set; }

    /// <summary>
    /// Аппаратный идентификатор робота
    /// </summary>
    public int RobotHardwareId { get; set; }

    /// <summary>
    /// "Тип робота (например, APR, AMR и т.д.)"
    /// </summary>
    public int RobotType { get; set; }

    /// <summary>
    /// "Наименование модели робота"
    /// </summary>
    public string RobotModel { get; set; }

    /// <summary>
    /// "Человекочитаемое имя робота"
    /// </summary>
    public string RobotName { get; set; }

    /// <summary>
    /// "IP-адрес робота"
    /// </summary>
    public string IP { get; set; }

    /// <summary>
    /// "Версия программного обеспечения робота (RCS)"
    /// </summary>
    public string SwVersion { get; set; }

    /// <summary>
    /// "Версия конструктивного исполнения робота"
    /// </summary>
    public string HwVersion { get; set; }

}
