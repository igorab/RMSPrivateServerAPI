using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMSPrivateServerAPI.Entities;

public class ppmtask
{
    [Key]
    public int id { get; set; }

    [ForeignKey("robotid")]
    public string robotid { get; set; }
    public string? taskdescription { get; set; }
    public DateTime scheduleddate { get; set; }

    // Другие свойства для задач ППР >>


    // Другие свойства для задач ППР <<

}
