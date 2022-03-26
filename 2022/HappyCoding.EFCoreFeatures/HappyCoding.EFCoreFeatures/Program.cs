
using HappyCoding.EFCoreFeatures.Data;
using HappyCoding.EFCoreFeatures.Util;
using Microsoft.EntityFrameworkCore;

namespace HappyCoding.EFCoreFeatures;

public static class Program
{
    // Parameters
    private const string CONNECTION_STRING = "Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=HappyCoding_2022_EFCoreFeatures;Integrated Security=SSPI";
    private const int ROW_COUNT = 1000;
    private const int RANDOM_SEED = 100;

    public static async Task Main()
    {
        Console.WriteLine("Create database");
        await DBUtil.EnsureNewDBAsync(CONNECTION_STRING);

        Console.WriteLine("Migrate database");
        var optionsBuilder = new DbContextOptionsBuilder<TestingDBContext>();
        optionsBuilder.UseSqlServer(CONNECTION_STRING);
        {
            await using var migrationContext = new TestingDBContext(optionsBuilder.Options);
            await migrationContext.Database.MigrateAsync();
        }
        Console.WriteLine("Database migrated");

        await FillDatabaseAsync(optionsBuilder.Options);
        await ManipulateSomeRowsAsync(optionsBuilder.Options);
    }

    private static async Task FillDatabaseAsync(DbContextOptions<TestingDBContext> dbContextOptions)
    {
        Console.WriteLine("Write values to database");
        await using var dbContext = new TestingDBContext(dbContextOptions);
        for (var loop = 0; loop < ROW_COUNT; loop++)
        {
            var newDocument = new TestingDocument();
            newDocument.Value1 = loop.ToString();
            newDocument.Value2 = loop.ToString();
            newDocument.Value3 = loop.ToString();

            await dbContext.Testing.AddAsync(new TestingRow(newDocument));
        }
        await dbContext.SaveChangesAsync();
    }

    private static async Task ManipulateSomeRowsAsync(DbContextOptions<TestingDBContext> dbContextOptions)
    {
        var random = new Random(RANDOM_SEED);

        Console.WriteLine("Manipulate some rows");
        for (var loop = 0; loop < 20; loop++)
        {
            await using var dbContext = new TestingDBContext(dbContextOptions);

            var idToFind = random.Next(0, ROW_COUNT - 1);
            var rowToManipulate = await dbContext.Testing
                .Where(x => x.ID == idToFind)
                .FirstAsync();

            rowToManipulate.Value2 = random.Next(0, 1000).ToString();

            await dbContext.SaveChangesAsync();
        }
    }
}
