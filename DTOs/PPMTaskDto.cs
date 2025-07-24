namespace RMSPrivateServerAPI.DTOs;

#pragma warning disable CS1591

// DTOs/PPMTaskDto.cs
public class PPMTaskDto
{
    public int Id { get; set; }
    public string RobotId { get; set; }
    public string? TaskDescription { get; set; }
    public DateTime ScheduledDate { get; set; }
}
