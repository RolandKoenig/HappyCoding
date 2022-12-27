using System.Text;

namespace HappyCoding.HttpClientServerCalls;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthorization();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();

        app.Map(
            "/dummyEndpoint",
            (HttpContext httpContext, ILogger<Program> logger) =>
            {
                var strBuilder = new StringBuilder(1024);
                foreach (var actHeader in httpContext.Request.Headers)
                {
                    strBuilder.AppendLine($"{actHeader.Key}: {actHeader.Value}");
                }

                logger.LogInformation(strBuilder.ToString());
            })
            .WithName("DummyEndpoint")
            .WithOpenApi();

        app.Run();
    }
}
