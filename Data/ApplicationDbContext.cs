using Microsoft.EntityFrameworkCore;
using RMSPrivateServerAPI.Entities;

namespace RMSPrivateServerAPI.Data;

// Data/ApplicationDbContext.cs
public class ApplicationDbContext : DbContext
{
    public DbSet<robotinfo> Robots { get; set; }
    public DbSet<PPMTask> PPMTasks { get; set; }
    public DbSet<FaultInfo> Faults { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<robotinfo>().ToTable("RobotInfo");
        modelBuilder.Entity<PPMTask>().ToTable("PPMTask");
        modelBuilder.Entity<RobotTask>().ToTable("RobotTask");
        modelBuilder.Entity<FaultInfo>().ToTable("FaultInfo");
    }
    
}
