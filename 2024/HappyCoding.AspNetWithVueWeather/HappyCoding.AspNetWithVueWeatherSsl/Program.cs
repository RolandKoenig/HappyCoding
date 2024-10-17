namespace HappyCoding.AspNetWithVueWeatherSsl;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseStaticFiles();
        app.UseRouting();
        
        app.UseAuthentication();
        app.UseAuthorization();
        
        app.MapControllers();
        app.MapFallbackToFile("index.html");
        
        app.Run();
    }
}