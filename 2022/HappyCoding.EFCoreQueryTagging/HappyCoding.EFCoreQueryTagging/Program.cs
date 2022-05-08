using System.Reflection;
using HappyCoding.EFCoreQueryTagging.Model;
using Microsoft.EntityFrameworkCore;

namespace HappyCoding.EFCoreQueryTagging;

public static class Program
{
    private const int RANDOM_SEED = 100;
    private const int COUNT_PROCESSES = 500;
    private const int COUNT_ACTIVITIES_PER_PROCESS = 2;

    public static async Task Main(string[] args)
    {
        var dbPath = Path.Combine(
            Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!,
            "test-database.db");
        if (File.Exists(dbPath))
        {
            File.Delete(dbPath);
        }

        var connectionString = $"Data Source={dbPath}";

        Console.WriteLine($"Creating database {dbPath}");
        var optionsBuilder = new DbContextOptionsBuilder<TestingDBContext>();
        optionsBuilder.UseSqlite(connectionString);
        {
            await using var migrationContext = new TestingDBContext(optionsBuilder.Options);
            await migrationContext.Database.MigrateAsync();
        }
        Console.WriteLine("Database created");

        var random = new Random(RANDOM_SEED);
        await PopulateDatabaseAsync(optionsBuilder.Options, random);
    }

    private static async Task PopulateDatabaseAsync(DbContextOptions<TestingDBContext> dbContextOptions, Random random)
    {
        await using var dbContext = new TestingDBContext(dbContextOptions);

        Console.WriteLine("Populate DB...");
        for (var processNumber = 0; processNumber < COUNT_PROCESSES; processNumber++)
        {
            var processKey = GetProcessKey(processNumber);

            var newProcess = new Procedure()
            {
                ID = processKey,
                CreateTimestampUtc = DateTime.UtcNow,
                Activities = new List<ProcedureActivity>(),
                Field1 = GetRandomInt(random),
                Field2 = GetRandomInt(random),
                Field3 = GetRandomInt(random),
                Field4 = GetRandomString(random),
                Field5 = GetRandomString(random),
                Field6 = GetRandomInt(random)
            };

            for (var loopActivity = 0; loopActivity < COUNT_ACTIVITIES_PER_PROCESS; loopActivity++)
            {
                newProcess.Activities.Add(new ProcedureActivity()
                {
                    ProcessID = processKey,
                    ActivityTimestampUtc = DateTime.UtcNow,
                    Field1 = GetRandomInt(random),
                    Field2 = GetRandomInt(random),
                    Field3 = GetRandomInt(random),
                    Field4 = GetRandomString(random),
                    Field5 = GetRandomString(random),
                    Field6 = GetRandomInt(random),
                });
            }

            await dbContext.Processes.AddAsync(newProcess);
        }
        Console.WriteLine("DB populated");

        await dbContext.SaveChangesAsync();
    }

    private static string GetProcessKey(int processNumber)
    {
        return "P" + processNumber.ToString("D7") + "1234" + processNumber.ToString("D7") + "12345678901";
    }

    private static int GetRandomInt(Random random)
    {
        return random.Next(0, 100000);
    }

    private static string GetRandomString(Random random)
    {
        return random.Next(0, 100000).ToString("D10");
    }
}



