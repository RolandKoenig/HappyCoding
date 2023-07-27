using HappyCoding.TestingWithContainers.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HappyCoding.TestingWithContainers.Migrations;

internal static class DbMigrator
{
    public static async Task MigratedDbAsync(string connectionString)
    {
        var dbContextBuilder = new DbContextOptionsBuilder<PersonDataDbContext>();
        dbContextBuilder.UseSqlServer(connectionString);
        var dbContext = new PersonDataDbContext(dbContextBuilder.Options);

        var dbMigrator = dbContext.Database.GetService<IMigrator>();

        await dbMigrator.MigrateAsync();
    }
}