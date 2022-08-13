using HappyCoding.HexagonalArchitecture.Application;
using HappyCoding.HexagonalArchitecture.SQLiteAdapter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace HappyCoding.HexagonalArchitecture.WebUI.Server
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
            builder.Services.AddSwaggerGen(options =>
                options.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "Hexagonal Architecture",
                    Version = "v1"
                }));
            builder.Services.AddMediatR(
                typeof(CreateWorkshopRequestHandler).Assembly);

            // Adapters
            builder.Services.AddSQLiteAdapter(
                builder.Configuration.GetConnectionString("WorkshopDB"));

            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            //##########
            // Configure request pipeline
            
            var app = builder.Build();
            
            if (app.Environment.IsDevelopment())
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

            app.UseHttpsRedirection();

            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();
            
            app.MapRazorPages();
            app.MapControllers();
            app.MapFallbackToFile("index.html");

            //##########
            // Run
            
            app.Run();
        }
    }
}