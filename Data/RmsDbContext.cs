namespace RMSPrivateServerAPI.Data;
#pragma warning disable CS1591
using Microsoft.EntityFrameworkCore;
using RMSPrivateServerAPI.Models;

/// <summary>
/// контекст базы данных
/// </summary>
public class RmsDbContext : DbContext
{
    public RmsDbContext(DbContextOptions<RmsDbContext> options) : base(options) { }
    
    public DbSet<RobotHeader> RobotHeaders { get; set; }

    public DbSet<Pose> Poses { get; set; }

    public DbSet<StateReport> StateReports { get; set; }
}


