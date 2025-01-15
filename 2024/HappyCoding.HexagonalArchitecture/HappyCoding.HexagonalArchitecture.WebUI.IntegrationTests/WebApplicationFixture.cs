using System.Reflection;
using HappyCoding.HexagonalArchitecture.WebUI.Server;
using Microsoft.AspNetCore.Mvc.Testing;

namespace HappyCoding.HexagonalArchitecture.WebUI.Integration.Tests;

public class WebApplicationFixture : WebApplicationFactory<Startup>
{ 
    public static readonly string HostEnvironment = "IntegrationTests";

    public WebApplicationFixture()
    {
        this.ResetDatabase();
    }

    private void ResetDatabase()
    {
        var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
        var dbPath = Path.Combine(assemblyPath, "workshopDatabase-integrationtest.db");
        if (File.Exists(dbPath))
        {
            File.Delete(dbPath);
        }
    }

    protected override IHostBuilder CreateHostBuilder()
    {
        return Program.CreateHostBuilder($"--Environment:{HostEnvironment}");
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        base.ConfigureWebHost(builder
            .ConfigureAppConfiguration((_, configBuilder) =>
            {
                configBuilder.SetBasePath(assemblyPath);
            })
            .UseEnvironment(HostEnvironment)
            .ConfigureServices(services =>
            {
                // Place for registering mocks
            }));
    }
}
