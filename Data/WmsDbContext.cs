using Microsoft.EntityFrameworkCore;
using RMSPrivateServerAPI.DTOs;
using RMSPrivateServerAPI.Entities;
using RMSPrivateServerAPI.Models;
using RMSPrivateServerAPI.StoreMapPOCO;
#pragma warning disable CS1591
namespace RMSPrivateServerAPI.Data;

/// <summary>
/// Data/ApplicationDbContext.cs
/// </summary>
public class WmsDbContext : DbContext
{
    public DbSet<robot_info> Robots { get; set; }
    public DbSet<ppm_task> PPMTasks { get; set; }
    public DbSet<FaultInfoDto> Faults { get; set; }

    public DbSet<robot_task> RobotTask { get; set; }

    // common task from WMS >>
    public DbSet<TasksDto> Tasks { get; set; }
    public DbSet<TaskActionsDto> TaskActions { get; set; }
    // common task from WMS <<
       
    public DbSet<RobotActionsDone> RobotActions { get; set; }

    // wms >>
    // DbSet для таблицы Store
    public DbSet<Store> Stores { get; set; }

    // DbSet для таблицы Area
    public DbSet<Area> Areas { get; set; }

    // DbSet для таблицы Zone
    public DbSet<Zone> Zones { get; set; }

    // DbSet для таблицы Point
    public DbSet<Point> Points { get; set; }

    // DbSet для таблицы Path
    public DbSet<StoreMapPOCO.Path> Paths { get; set; }

    // DbSet для таблицы PathElement
    public DbSet<PathElement> PathElements { get; set; }

    // DbSet для таблицы ZoneType
    public DbSet<ZoneType> ZoneTypes { get; set; }

    // DbSet для таблицы PointType
    public DbSet<PointType> PointTypes { get; set; }

    // DbSet для таблицы PathElementType
    public DbSet<PathElementType> PathElementTypes { get; set; }
    // wms >>

    public WmsDbContext(DbContextOptions<WmsDbContext> options)
        : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<robot_info>().ToTable("RobotInfo");

        modelBuilder.Entity<ppm_task>().ToTable("PPMTask");

        modelBuilder.Entity<robot_task>().ToTable("RobotTask");

        modelBuilder.Entity<RobotActionsDone>().ToTable("RobotActions");

        modelBuilder.Entity<FaultInfoDto>().ToTable("FaultInfo");

        modelBuilder.Entity<TasksDto>().ToTable("Tasks");       
    }
    
}
