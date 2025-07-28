using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMSPrivateServerAPI.Entities;
#pragma warning disable CS1591, IDE1006

[Table("robot_actions")]
public class robot_task_actions_flat : robot_task
{
    [Key]
    public string? action_id { get; set; }

    [ForeignKey("taskid")]
    public string? robot_task_id { get; set; }

    [Column("title")]       
    public string? title { get; set; }
    
    public int  action_type { get; set; }

    public float? pose_X { get; set; }

    public float? pose_Y { get; set; }

    public float? heading { get; set; }

    public int? direction { get; set; }

    public float? distance { get; set; }

    public float? angle { get; set; }

    public float? radius { get; set; }
}
