namespace RMSPrivateServerAPI.Data;
using Microsoft.EntityFrameworkCore;
using RMSPrivateServerAPI.StoreMapDto;

#pragma warning disable CS1591
public class WarehouseDbContext : DbContext
{
    public WarehouseDbContext(DbContextOptions<WarehouseDbContext> options) : base(options)
    {
    }

    // DbSet для таблицы Store
    public DbSet<Store> Stores { get; set; }

    // DbSet для таблицы Area
    public DbSet<Area> Areas { get; set; }

    // DbSet для таблицы Zone
    public DbSet<Zone> Zones { get; set; }

    // DbSet для таблицы Point
    public DbSet<Point> Points { get; set; }

    // DbSet для таблицы Path
    public DbSet<Path> Paths { get; set; }

    // DbSet для таблицы PathElement
    public DbSet<PathElement> PathElements { get; set; }

    // DbSet для таблицы ZoneType
    public DbSet<ZoneType> ZoneTypes { get; set; }

    // DbSet для таблицы PointType
    public DbSet<PointType> PointTypes { get; set; }

    // DbSet для таблицы PathElementType
    public DbSet<PathElementType> PathElementTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Здесь можно настроить дополнительные параметры для сущностей, если это необходимо
        base.OnModelCreating(modelBuilder);
    }
}
