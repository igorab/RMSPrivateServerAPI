using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace RMSPrivateServerAPI.Entities;

#pragma warning disable IDE1006
#pragma warning disable CS8618
#pragma warning disable CS8981

/// <summary>
/// Справочник роботов (id и прочие данные, характеризующие конкретный экземпляр робота)
/// </summary>
[Table("RobotInfo")]
public class robot_info
{
    /// <summary>
    /// "Уникальный идентификатор робота (PRIMARY KEY)"
    /// </summary>
    [Key]
    public Guid RobotId { get; set; }

    /// <summary>
    /// Аппаратный идентификатор робота
    /// </summary>
    [Column("RobotHardwareId")]
    public int robothardwareid { get; set; }

    /// <summary>
    /// "Тип робота (например, APR, AMR и т.д.)"
    /// </summary>
    [Column("RobotType")]
    public string robottype { get; set; }

    /// <summary>
    /// "Наименование модели робота"
    /// </summary>
    [Column("RobotModel")]
    public string robotmodel { get; set; }

    /// <summary>
    /// "Человекочитаемое имя робота"
    /// </summary>
    [Column("RobotName")]
    public string robotname { get; set; }

    /// <summary>
    /// "IP-адрес робота"
    /// </summary>
    [Column("IP")]
    public string ip { get; set; }

    /// <summary>
    /// "Версия программного обеспечения робота (RCS)"
    /// </summary>
    [Column("SwVersion")]
    public string swversion { get; set; }

    /// <summary>
    /// "Версия конструктивного исполнения робота"
    /// </summary>
    [Column("HwVersion")]
    public string hwversion { get; set; }

    /// <summary>
    /// состояние робота
    /// </summary>
    [Column("RobotState")]
    public int? robot_state { get; set; } = 0;   
}
