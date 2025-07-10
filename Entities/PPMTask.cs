namespace RMSPrivateServerAPI.Entities;

public class PPMTask
{
    public int Id { get; set; }
    public int RobotId { get; set; }
    public string? TaskDescription { get; set; }
    public DateTime ScheduledDate { get; set; }

    // Другие свойства для задач ППР >>


    // Другие свойства для задач ППР <<

}
