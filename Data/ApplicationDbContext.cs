using Microsoft.EntityFrameworkCore;
using RMSPrivateServerAPI.Entities;

namespace RMSPrivateServerAPI.Data;

// Data/ApplicationDbContext.cs
public class ApplicationDbContext : DbContext
{
    public DbSet<RobotInfo> Robots { get; set; }
    public DbSet<PPMTask> PPMTasks { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RobotInfo>().ToTable("Robots");
        modelBuilder.Entity<PPMTask>().ToTable("PPMTasks");
    }
    
}
