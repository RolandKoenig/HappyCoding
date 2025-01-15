using HappyCoding.HexagonalArchitecture.Application;
using HappyCoding.HexagonalArchitecture.SQLiteAdapter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace HappyCoding.HexagonalArchitecture.WebUI.Server;

public class Startup
{
    public IConfiguration Configuration { get; }
    
    public Startup(IConfiguration configuration)
    {
        this.Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllersWithViews();
        // services.AddRazorPages();
        services.AddRazorComponents().AddInteractiveWebAssemblyComponents();
        services.AddSwaggerGen(options =>
            options.SwaggerDoc("v1", new OpenApiInfo()
            {
                Title = "Hexagonal Architecture",
                Version = "v1"
            }));
        services.AddMediatR(
            config => config.RegisterServicesFromAssembly(typeof(CreateWorkshopRequestHandler).Assembly));

        services.AddSQLiteAdapter(
            this.Configuration.GetConnectionString("WorkshopDB"));

        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });
    }

    public void Configure(IApplicationBuilder app, IHostEnvironment env)
    {
        if (env.IsDevelopment())
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
        
        app.UseEndpoints(endpoints =>
        {
            // endpoints.MapRazorPages();
            endpoints.MapControllers();
            endpoints.MapRazorComponents<App>()
                .AddInteractiveWebAssemblyRenderMode()
                .AddAdditionalAssemblies(typeof(HappyCoding.HexagonalArchitecture.WebUI.Client._Imports).Assembly);
        });
    }
}