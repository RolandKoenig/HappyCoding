using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HappyCoding.EFCoreMultipleContextsInSameDb.Context1;

public class DbMigrator
{
    public static async Task MigrateDbAsync(string connectionString)
    {
        var dbContextBuilder = new DbContextOptionsBuilder<DataContext1>();
        dbContextBuilder.UseSqlServer(
            connectionString,
            config =>
            {
                config.MigrationsHistoryTable("Migrations", "ctx1"); 
            });
        var dbContext = new DataContext1(dbContextBuilder.Options);

        var dbMigrator = dbContext.Database.GetService<IMigrator>();

        await dbMigrator.MigrateAsync();
    }
}
