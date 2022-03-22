using Microsoft.EntityFrameworkCore;

namespace HappyCoding.SqlServerPartitionedSensorDataTable;

public class SensorDataDbContext : DbContext
{
    public DbSet<SensorData> SensorData { get; set; } = null!;

    public SensorDataDbContext(DbContextOptions options)
        : base(options)
    {

    }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var tableSensorData = modelBuilder.Entity<SensorData>().ToTable("SensorData");
        tableSensorData
            .HasKey(x => x.ID)
            .IsClustered(false);
        tableSensorData
            .HasIndex(x => x.Timestamp, "IX_TIMESTAMP")
            .IsClustered(true);
        tableSensorData.Property(x => x.ID);
        tableSensorData.Property(x => x.Timestamp);
        tableSensorData
            .Property(x => x.SensorName)
            .HasMaxLength(30);
        tableSensorData.Property(x => x.SensorValue);
    }
}