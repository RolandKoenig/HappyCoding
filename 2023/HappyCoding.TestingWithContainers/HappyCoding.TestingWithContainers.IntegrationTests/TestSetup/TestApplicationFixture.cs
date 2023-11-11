using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.SqlClient;

namespace HappyCoding.TestingWithContainers.IntegrationTests.TestSetup;

public class TestApplicationFixture : WebApplicationFactory<Program>
{
    private static readonly string HostEnvironment = "IntegrationTests";

    private string? _sqlConnectionString;
    private IContainer? _sqlEdgeContainer;
    
    /// <summary>
    /// Cleanup all data in databases.
    /// </summary>
    public async Task CleanupDatabaseAsync()
    {
        if (string.IsNullOrEmpty(_sqlConnectionString))
        {
            throw new InvalidOperationException("Container not started. Create a client before calling this method!");
        }
        
        await using var connection = new SqlConnection(_sqlConnectionString);
        await connection.OpenAsync();

        await using var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM dbo.Persons";
        await command.ExecuteNonQueryAsync();
    }
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        this.EnsureContainersStarted();
        
        builder.UseEnvironment(HostEnvironment);
        builder.UseSetting("ConnectionStrings:PersonDb", _sqlConnectionString);
    }

    /// <summary>
    /// Start all required containers for testing the target application.
    /// </summary>
    private void EnsureContainersStarted()
    {
        if (_sqlEdgeContainer != null) { return; }
        
        // Start sql edge container
        var azureSqlTag = ":2.0.0";
        if (OperatingSystem.IsMacOS()) { azureSqlTag = "";}
        _sqlEdgeContainer = new ContainerBuilder()
            .WithImage($"mcr.microsoft.com/azure-sql-edge{azureSqlTag}")
            .WithEnvironment("ACCEPT_EULA", "Y")
            .WithEnvironment("MSSQL_USER", "SA")
            .WithEnvironment("MSSQL_SA_PASSWORD", "MySecret@Password123?")
            .WithEnvironment("MSSQL_PID", "Developer")
            .WithPortBinding(1433, true)
            .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(1433))
            .Build();
        _sqlEdgeContainer.StartAsync().Wait();

        var sqlPort = _sqlEdgeContainer.GetMappedPublicPort(1433);
        _sqlConnectionString =
            $"Data Source=localhost,{sqlPort};Initial Catalog=HappyCoding_TestingWithContainers;Integrated Security=SSPI;User Id=SA;Password=MySecret@Password123?;Trusted_Connection=False;Encrypt=False;";
    }
}