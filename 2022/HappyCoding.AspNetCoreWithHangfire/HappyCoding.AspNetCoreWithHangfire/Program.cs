using System.Reflection;
using Hangfire;

namespace HappyCoding.AspNetCoreWithHangfire.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var appRootPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var appOptions = new WebApplicationOptions()
        {
            Args = args,
            EnvironmentName = "Development",
            ContentRootPath = appRootPath
        };

        var builder = WebApplication.CreateBuilder(appOptions);
        var services = builder.Services;
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddHangfire(configuration => configuration
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(builder.Configuration.GetConnectionString("HangfireDB")));

        var app = builder.Build();

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.UseHangfireDashboard();
        app.MapControllers();
        app.Run();
    }
}
