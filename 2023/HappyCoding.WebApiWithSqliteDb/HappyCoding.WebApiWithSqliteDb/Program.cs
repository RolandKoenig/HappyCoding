using System.Reflection;
using HappyCoding.WebApiWithSqliteDb.Data;
using HappyCoding.WebApiWithSqliteDb.Migrations;
using Microsoft.EntityFrameworkCore;

namespace HappyCoding.WebApiWithSqliteDb;

public class Program
{
    public static async Task Main(string[] args)
    {
        var appRootPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        Environment.CurrentDirectory = appRootPath!;

        var builder = WebApplication.CreateBuilder(args);

        // Migrate database
        var dbConnectionString = builder.Configuration.GetConnectionString("PersonDb");
        await DbMigrator.MigratedDbAsync(dbConnectionString!);

        // #######################
        // Load services
        builder.Services
            .AddDbContext<PersonDataDbContext>(
                options => options.UseSqlite(dbConnectionString));

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // #######################
        // Configure request pipeline

        var app = builder.Build();
        app.UseSwagger();
        app.UseSwaggerUI();
        app.MapControllers();
        
        await app.RunAsync();
    }
}
