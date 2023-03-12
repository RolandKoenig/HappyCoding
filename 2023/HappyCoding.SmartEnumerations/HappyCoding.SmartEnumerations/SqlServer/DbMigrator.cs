using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HappyCoding.SmartEnumerations.SqlServer;

public static class DbMigrator
{
    public static async Task MigrateDbAsync(string dbConnectionString)
    {
        var dbContextBuilder = new DbContextOptionsBuilder<TestingDbContext>();
        dbContextBuilder.UseSqlServer(dbConnectionString);
        var dbContext = new TestingDbContext(dbContextBuilder.Options);

        var dbMigrator = dbContext.Database.GetService<IMigrator>();

        await dbMigrator.MigrateAsync();
    }
}
