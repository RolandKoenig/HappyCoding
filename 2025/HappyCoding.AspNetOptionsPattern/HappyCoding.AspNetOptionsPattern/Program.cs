using Microsoft.Extensions.Options;

namespace HappyCoding.AspNetOptionsPattern;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddOptions<ApplicationOptions>()
            .Bind(builder.Configuration.GetSection(ApplicationOptions.SECTION_NAME))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        var app = builder.Build();
        app.MapGet("/OptionsUsingIOptions", (IOptions<ApplicationOptions> options) => options.Value);
        app.MapGet("/OptionsUsingIOptionsSnapshop", (IOptionsSnapshot<ApplicationOptions> options) => options.Value);
        app.MapGet("/OptionsUsingIOptionsMonitor", (IOptionsMonitor<ApplicationOptions> options) => options.CurrentValue);
        app.Run();
    }
}