using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#pragma warning disable CS1591
namespace RMSPrivateServerAPI.Entities;

public class ppmtask
{
    [Key]
    public int id { get; set; }

    [ForeignKey("robotid")]
    [Column("robotid")]
    public string robotid { get; set; }

    [Column("taskdescription")]
    public string? taskdescription { get; set; }

    [Column("scheduleddate")]
    public DateTime scheduleddate { get; set; }

    // Другие свойства для задач ППР >>


    // Другие свойства для задач ППР <<

}
