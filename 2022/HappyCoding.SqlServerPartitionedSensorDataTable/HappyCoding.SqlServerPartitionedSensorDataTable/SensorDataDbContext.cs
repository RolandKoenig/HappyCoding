using Microsoft.EntityFrameworkCore;

namespace HappyCoding.SqlServerPartitionedSensorDataTable;

public class SensorDataDbContext : DbContext
{
    public DbSet<SensorData> SensorData { get; set; } = null!;

    public SensorDataDbContext(DbContextOptions options)
        : base(options)
    {

    }
}