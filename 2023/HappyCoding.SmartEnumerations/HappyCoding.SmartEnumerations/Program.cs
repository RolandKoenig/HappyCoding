using HappyCoding.SmartEnumerations.Data;
using HappyCoding.SmartEnumerations.Enums;
using HappyCoding.SmartEnumerations.SqlServer;
using Microsoft.EntityFrameworkCore;

namespace HappyCoding.SmartEnumerations;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var dbConnectionString = "Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=HappyCoding_2023_SmartEnumerations;Integrated Security=SSPI";

        await DbMigrator.MigrateDbAsync(dbConnectionString);

        await using var writingDbContext = CreateDbContext(dbConnectionString);

        // Clear current data
        await writingDbContext.Addresses.ExecuteDeleteAsync();

        // Create new rows
        var random =new Random(0);
        for (var loop = 0; loop < 100; loop++)
        {
            var actAddress = new Address();
            actAddress.Country = Nation.List.ElementAt(random.Next(0, Nation.List.Count));
            actAddress.Name = "TestName";
            actAddress.City = "TestCity";
            actAddress.PostalCode = "12345";
            actAddress.Street = "TestStreet";
            await writingDbContext.Addresses.AddAsync(actAddress);
        }
        await writingDbContext.SaveChangesAsync();

        // Read all data from database
        await using var readingDbContext = CreateDbContext(dbConnectionString);
        foreach (var actAddress in await readingDbContext.Addresses
                     .Where(x => x.Country != Nation.Germany)
                     .ToListAsync())
        {
            Console.WriteLine($"{actAddress.Name}, {actAddress.Street}, {actAddress.PostalCode}, {actAddress.City}, {actAddress.Country}");
        }
    }

    private static TestingDbContext CreateDbContext(string dbConnectionString)
    {
        var optionsBuilder =
            new DbContextOptionsBuilder<TestingDbContext>();
        optionsBuilder.UseSqlServer(dbConnectionString);
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.LogTo(
            logLine =>
            {
                Console.WriteLine();
                Console.WriteLine("Sending SQL...");

                var prefColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine(logLine);
                Console.ForegroundColor = prefColor;

                Console.WriteLine();
            },
            (eventId, logLevel) =>
            {
                return eventId.Id == 20101;
            });
        return new TestingDbContext(optionsBuilder.Options);
    }
}
