using HappyCoding.HexagonalArchitecture.Application.UseCases;
using HappyCoding.HexagonalArchitecture.SQLiteAdapter;
using HappyCoding.HexagonalArchitecture.WebUI.Server.Middlewares;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace HappyCoding.HexagonalArchitecture.WebUI.Server;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // ###### Configure services
        builder.Services.AddControllersWithViews();

        builder.Services.AddRazorComponents().AddInteractiveWebAssemblyComponents();
        builder.Services.AddSwaggerGen(options =>
            options.SwaggerDoc("v1", new OpenApiInfo()
            {
                Title = "Hexagonal Architecture",
                Version = "v1"
            }));
        builder.Services.AddMediatR(
            config => config.RegisterServicesFromAssembly(typeof(CreateWorkshopCommandHandler).Assembly));

        builder.Services.AddSQLiteAdapter(
            builder.Configuration.GetConnectionString("WorkshopDB")!);

        builder.Services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });
        
        // ###### Configure request pipeline
        var app = builder.Build();
        app.UseMiddleware<RequestLoggingMiddleware>();
        
        if (builder.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hexagonal Architecture v1");
            });

            app.UseWebAssemblyDebugging();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }
        
        app.UseStaticFiles();

        app.UseRouting();
        app.UseAntiforgery();

        app.MapControllers();
        app.MapRazorComponents<App>()
            .AddInteractiveWebAssemblyRenderMode()
            .AddAdditionalAssemblies(typeof(HappyCoding.HexagonalArchitecture.WebUI.Client.Program).Assembly);

        await app.RunAsync();
    }
}