using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HappyCoding.SqlServerPartitionedSensorDataTable;

internal class SensorDataDbContextFactory : IDesignTimeDbContextFactory<SensorDataDbContext>
{
    /// <inheritdoc />
    public SensorDataDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<SensorDataDbContext>();
        optionsBuilder.UseSqlServer();

        return new SensorDataDbContext(optionsBuilder.Options);
    }
}