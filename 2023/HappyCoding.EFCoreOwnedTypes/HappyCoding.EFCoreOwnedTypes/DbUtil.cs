using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HappyCoding.EFCoreOwnedTypes;

public class DbUtil
{
    public static MyDbContext CreateDbContext(string dbConnectionString, bool doLog = false)
    {
        var dbContextBuilder = new DbContextOptionsBuilder<MyDbContext>();
        dbContextBuilder.UseSqlServer(
            dbConnectionString,
            config =>
            {
                config.MigrationsHistoryTable("Migrations"); 
            });

        if (doLog)
        {
            dbContextBuilder.LogTo(Console.WriteLine, (eventId, _) => eventId.Id == 20100);
            dbContextBuilder.EnableSensitiveDataLogging();
        }

        return new MyDbContext(dbContextBuilder.Options);
    }

    public static async Task MigrateDbAsync(string dbConnectionString)
    {
        var dbContext = CreateDbContext(dbConnectionString);

        var dbMigrator = dbContext.Database.GetService<IMigrator>();

        await dbMigrator.MigrateAsync();
    }
}
