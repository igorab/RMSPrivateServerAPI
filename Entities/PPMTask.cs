using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#pragma warning disable CS1591
namespace RMSPrivateServerAPI.Entities;

[Table("\"PPMTask\"")]
public class ppmtask
{
    [Key]
    [Column("Id")]
    public Guid id { get; set; }
    
    [Column("RobotId")]
    public Guid robotid { get; set; }

    [Column("TaskDescription")]
    public string? taskdescription { get; set; }

    [Column("ScheduledDate")]
    public DateTime scheduleddate { get; set; }

    // Другие свойства для задач ППР >>


    // Другие свойства для задач ППР <<

}
