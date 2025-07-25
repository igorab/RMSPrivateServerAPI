using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMSPrivateServerAPI.Entities;
#pragma warning disable CS1591
public class robot_task_actions_flat : robot_task
{
    [Key]
    public string? action_id { get; set; }

    [ForeignKey("taskid")]
    public string? task_id { get; set; }

    public string? action_name { get; set; }

    public int  action_type { get; set; }

    public float? action_value { get; set; }
}
