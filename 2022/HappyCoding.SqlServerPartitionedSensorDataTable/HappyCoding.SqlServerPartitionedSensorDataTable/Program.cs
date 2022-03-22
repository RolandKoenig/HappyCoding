using HappyCoding.SqlServerPartitionedSensorDataTable.Util;
using Microsoft.EntityFrameworkCore;

namespace HappyCoding.SqlServerPartitionedSensorDataTable;

public static class Program
{
    // Parameter
    internal const int RANDOM_SEED = 100;
    internal const string CONNECTION_STRING =
        "Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=HappyCoding_2022_SqlServerPartitioned;Integrated Security=SSPI";
    internal const string START_TIMESTAMP = "2022-03-21T18:54:54.3281670+00:00";

    public static async Task Main()
    {
        Console.WriteLine("Creating DB..");
        await DBUtil.EnsureNewDBAsync(CONNECTION_STRING);

        var optionsBuilder = new DbContextOptionsBuilder<SensorDataDbContext>();
        optionsBuilder.UseSqlServer(CONNECTION_STRING);
        {
            await using var migrationContext = new SensorDataDbContext(optionsBuilder.Options);
            await migrationContext.Database.MigrateAsync();
        }

        Console.WriteLine("Adding values..");
        var random = new Random(RANDOM_SEED);
        var startTimestamp = DateTimeOffset.Parse(START_TIMESTAMP);
        var singleStep = TimeSpan.FromMilliseconds(100);
        for (var loop = 0; loop < 10000; loop++)
        {
            await using var dbContext = new SensorDataDbContext(optionsBuilder.Options);

            await dbContext.SensorData.AddAsync(new SensorData()
            {
                ID = Guid.NewGuid(),
                Timestamp = startTimestamp + (loop * singleStep),
                SensorName = "TestSensor",
                SensorValue = random.NextSingle() * 1000f
            },
                CancellationToken.None);
            await dbContext.SaveChangesAsync(CancellationToken.None);
        }
    }
}