using HappyCoding.HexagonalArchitecture.SQLiteAdapter;
using Microsoft.AspNetCore.ResponseCompression;

namespace HappyCoding.HexagonalArchitecture.WebUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //##########
            // Configure services
            
            // Infrastructure
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            // Adapters
            builder.Services.AddSQLiteAdapter(
                builder.Configuration.GetConnectionString("WorkshopDB"));

            //##########
            // Configure request pipeline
            
            var app = builder.Build();
            
            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();
            
            app.MapRazorPages();
            app.MapControllers();
            app.MapFallbackToFile("index.html");

            app.Run();
        }
    }
}