using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMSPrivateServerAPI.Entities;
#pragma warning disable CS1591, IDE1006

[Table("robot_actions")]
public class RobotTaskFlat : robot_task
{    
    public Guid action_id { get; set; }

    [ForeignKey("TaskId")]
    [Column("TaskId")]
    public Guid robot_task_id { get; set; }

    [Column("title")]       
    public string? title { get; set; }
    
    public int  action_type { get; set; }

    public float? pose_x { get; set; }

    public float? pose_y { get; set; }

    public float? heading { get; set; }

    public int? direction { get; set; }

    public float? distance { get; set; }

    public float? angle { get; set; }

    public float? radius { get; set; }
}
