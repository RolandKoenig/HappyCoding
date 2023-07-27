using HappyCoding.TestingWithContainers.Data;
using HappyCoding.TestingWithContainers.Migrations;
using Microsoft.EntityFrameworkCore;

namespace HappyCoding.TestingWithContainers;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Migrate database
        var dbConnectionString = builder.Configuration.GetConnectionString("PersonDb");
        await DbMigrator.MigratedDbAsync(dbConnectionString!);

        // #######################
        // Load services
        builder.Services
            .AddDbContext<PersonDataDbContext>(
                options => options.UseSqlServer(dbConnectionString));

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // #######################
        // Configure request pipeline

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();


        app.MapControllers();

        // #######################
        // Run

        await app.RunAsync();
    }
}
