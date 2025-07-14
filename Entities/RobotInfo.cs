using System.ComponentModel;

namespace RMSPrivateServerAPI.Entities;

/// <summary>
/// Справочник роботов (id и прочие данные, характеризующие конкретный экземпляр робота)
/// </summary>
public class RobotInfo
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }

    // Другие свойства, характеризующие робота >>
    [Description("Уникальный идентификатор робота (PRIMARY KEY)")]
    public int RobotID { get; set; }

    [Description("Тип робота (например, APR, AMR и т.д.)")]
    public string RobotType { get; set; }

    [Description("Наименование модели робота")]
    public string RobotModel { get; set; }

    [Description("Человекочитаемое имя робота")]
    public string RobotName { get; set; }

    [Description("IP-адрес робота")]
    public string IP { get; set; }

    [Description("Версия программного обеспечения робота (RCS)")]
    public string SwVersion { get; set; }

    [Description("Версия конструктивного исполнения робота")]
    public string HwVersion { get; set; }
    // Другие свойства, характеризующие робота <<
}
