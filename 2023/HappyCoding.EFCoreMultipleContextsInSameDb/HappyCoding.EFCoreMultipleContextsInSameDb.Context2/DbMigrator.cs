using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HappyCoding.EFCoreMultipleContextsInSameDb.Context2;

public class DbMigrator
{
    public static async Task MigrateDbAsync(string connectionString)
    {
        var dbContextBuilder = new DbContextOptionsBuilder<DataContext2>();
        dbContextBuilder.UseSqlServer(
            connectionString,
            config =>
            {
                config.MigrationsHistoryTable("Migrations", "ctx2"); 
            });
        var dbContext = new DataContext2(dbContextBuilder.Options);

        var dbMigrator = dbContext.Database.GetService<IMigrator>();

        await dbMigrator.MigrateAsync();
    }
}
