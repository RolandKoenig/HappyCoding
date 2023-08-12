using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HappyCoding.EFCoreOwnedTypes;

public class DbUtil
{
    public static MyDbContext CreateDbContext(string dbConnectionString)
    {
        var dbContextBuilder = new DbContextOptionsBuilder<MyDbContext>();
        dbContextBuilder.UseSqlServer(
            dbConnectionString,
            config =>
            {
                config.MigrationsHistoryTable("Migrations"); 
            });
        return new MyDbContext(dbContextBuilder.Options);
    }

    public static async Task MigrateDbAsync(string dbConnectionString)
    {
        var dbContext = CreateDbContext(dbConnectionString);

        var dbMigrator = dbContext.Database.GetService<IMigrator>();

        await dbMigrator.MigrateAsync();
    }
}
