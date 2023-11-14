using HappyCoding.WebApiWithSqliteDb.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HappyCoding.WebApiWithSqliteDb.Migrations;

internal static class DbMigrator
{
    public static async Task MigratedDbAsync(string connectionString)
    {
        var dbContextBuilder = new DbContextOptionsBuilder<PersonDataDbContext>();
        dbContextBuilder.UseSqlite(connectionString);
        var dbContext = new PersonDataDbContext(dbContextBuilder.Options);

        var dbMigrator = dbContext.Database.GetService<IMigrator>();
        
        await dbMigrator.MigrateAsync();
    }
}