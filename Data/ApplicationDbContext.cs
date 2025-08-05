using Microsoft.EntityFrameworkCore;
using RMSPrivateServerAPI.DTOs;
using RMSPrivateServerAPI.Entities;
using RMSPrivateServerAPI.Models;
#pragma warning disable CS1591
namespace RMSPrivateServerAPI.Data;

/// <summary>
/// Data/ApplicationDbContext.cs
/// </summary>
public class ApplicationDbContext : DbContext
{
    public DbSet<robot_info> Robots { get; set; }
    public DbSet<ppmtask> PPMTasks { get; set; }
    public DbSet<FaultInfoDto> Faults { get; set; }
    // common task from WMS >>
    public DbSet<TasksDto> Tasks { get; set; }
    public DbSet<TaskActionsDto> TaskActions { get; set; }
    // common task from WMS <<
    public DbSet<robot_task> RobotTask { get; set; }
    public DbSet<RobotTaskFlat> RobotTaskActions { get; set; }

    public DbSet<RobotActionsDto> RobotActions { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<robot_info>().ToTable("RobotInfo");

        modelBuilder.Entity<ppmtask>().ToTable("PPMTask");

        modelBuilder.Entity<robot_task>().ToTable("RobotTask");

        modelBuilder.Entity<RobotTaskFlat>().ToTable("RobotTaskActions");

        modelBuilder.Entity<FaultInfoDto>().ToTable("FaultInfo");

        modelBuilder.Entity<TasksDto>().ToTable("Tasks");

        modelBuilder.Entity<RobotActionsDto>().ToTable("RobotActions");
    }
    
}
