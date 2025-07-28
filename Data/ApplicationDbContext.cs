using Microsoft.EntityFrameworkCore;
using RMSPrivateServerAPI.Entities;

namespace RMSPrivateServerAPI.Data;

/// <summary>
/// Data/ApplicationDbContext.cs
/// </summary>
public class ApplicationDbContext : DbContext
{
    public DbSet<robotinfo> Robots { get; set; }
    public DbSet<ppmtask> PPMTasks { get; set; }
    public DbSet<FaultInfoDto> Faults { get; set; }
    public DbSet<robot_task> Tasks { get; set; }
    public DbSet<RobotTaskFlat> RobotTaskActions { get; set; }


    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<robotinfo>().ToTable("RobotInfo");

        modelBuilder.Entity<ppmtask>().ToTable("PPMTask");

        modelBuilder.Entity<robot_task>().ToTable("RobotTask");

        modelBuilder.Entity<RobotTaskFlat>().ToTable("RobotTaskActions");

        modelBuilder.Entity<FaultInfoDto>().ToTable("FaultInfo");
    }
    
}
