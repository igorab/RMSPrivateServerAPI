namespace RMSPrivateServerAPI.DTOs
{
    // DTOs/PPMTaskDto.cs
    public class PPMTaskDto
    {
        public int Id { get; set; }
        public int RobotId { get; set; }
        public string? TaskDescription { get; set; }
        public DateTime ScheduledDate { get; set; }
    }
}
