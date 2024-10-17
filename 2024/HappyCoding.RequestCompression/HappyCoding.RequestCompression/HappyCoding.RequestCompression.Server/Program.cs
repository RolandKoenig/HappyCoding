using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace HappyCoding.RequestCompression.Server;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddRequestDecompression();

        var app = builder.Build();
        // app.UseRequestDecompression();
        
        app.MapPost("/hello", async ([FromServices] ILogger<Program> logger, HttpContext context) =>
        {
            var strBuilder = new StringBuilder(2048);
            foreach (var actHeader in context.Request.Headers)
            {
                strBuilder.AppendLine($"{actHeader.Key}: {actHeader.Value}");
            }
            strBuilder.AppendLine();

            using var streamReader = new StreamReader(context.Request.Body);
            strBuilder.Append(await streamReader.ReadToEndAsync());
            
            logger.LogInformation(strBuilder.ToString());

            return Results.Ok();
        });

        app.Run();
    }
}



