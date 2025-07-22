using System.ComponentModel;
namespace RMSPrivateServerAPI.Entities;

#pragma warning disable IDE1006
#pragma warning disable CS8618
#pragma warning disable CS8981

/// <summary>
/// Справочник роботов (id и прочие данные, характеризующие конкретный экземпляр робота)
/// </summary>
public class robotinfo
{               
    /// <summary>
    /// "Уникальный идентификатор робота (PRIMARY KEY)"
    /// </summary>
    public int robotid { get; set; }

    /// <summary>
    /// Аппаратный идентификатор робота
    /// </summary>
    public int robothardwareId { get; set; }

    /// <summary>
    /// "Тип робота (например, APR, AMR и т.д.)"
    /// </summary>
    public int robottype { get; set; }

    /// <summary>
    /// "Наименование модели робота"
    /// </summary>
    public string robotmodel { get; set; }

    /// <summary>
    /// "Человекочитаемое имя робота"
    /// </summary>
    public string robotname { get; set; }

    /// <summary>
    /// "IP-адрес робота"
    /// </summary>
    public string ip { get; set; }

    /// <summary>
    /// "Версия программного обеспечения робота (RCS)"
    /// </summary>
    public string swversion { get; set; }

    /// <summary>
    /// "Версия конструктивного исполнения робота"
    /// </summary>
    public string hwversion { get; set; }

    public bool? is_deleted { get; set; }    
}
