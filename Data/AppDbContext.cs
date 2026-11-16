using AgroAlerta.Models;
using Microsoft.EntityFrameworkCore;

namespace AgroAlerta.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Farm> Farms => Set<Farm>();

    public DbSet<SensorReading> SensorReadings => Set<SensorReading>();

    public DbSet<Alert> Alerts => Set<Alert>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Farm>()
            .HasMany(f => f.Readings)
            .WithOne(r => r.Farm)
            .HasForeignKey(r => r.FarmId);

        modelBuilder.Entity<Farm>()
            .HasMany(f => f.Alerts)
            .WithOne(a => a.Farm)
            .HasForeignKey(a => a.FarmId);
    }
}