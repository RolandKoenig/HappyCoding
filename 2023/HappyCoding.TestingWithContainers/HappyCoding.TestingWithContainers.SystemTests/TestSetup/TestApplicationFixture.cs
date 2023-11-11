using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Microsoft.Data.SqlClient;

namespace HappyCoding.TestingWithContainers.SystemTests.TestSetup;

public class TestApplicationFixture
{
    private IContainer[]? _containers;

    private string? _sqlConnectionString;
    private string? _sqlConnectionStringFromPublic;
    private string? _applicationBaseUrl;

    public string SqlConnectionString => _sqlConnectionString ?? string.Empty;

    public string ApplicationBaseUrl => _applicationBaseUrl ?? string.Empty;

    public async Task EnsureContainersStartedAsync()
    {
        if (_containers != null)
        {
            await CleanupDatabaseAsync();
            return;
        }

        const string DATABASE_HOST_NAME = "sql_database";

        var dockerNetwork = new NetworkBuilder()
            .Build();

        var azureSqlTag = ":2.0.0";
        if (OperatingSystem.IsMacOS())
        {
            azureSqlTag = "";
        }

        var sqlEdgeContainer = new ContainerBuilder()
            .WithImage($"mcr.microsoft.com/azure-sql-edge{azureSqlTag}")
            .WithNetwork(dockerNetwork)
            .WithNetworkAliases(DATABASE_HOST_NAME)
            .WithEnvironment("ACCEPT_EULA", "Y")
            .WithEnvironment("MSSQL_USER", "SA")
            .WithEnvironment("MSSQL_SA_PASSWORD", "MySecret@Password123?")
            .WithEnvironment("MSSQL_PID", "Developer")
            .WithPortBinding(1433, true)
            .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(1433))
            .Build();

        _sqlConnectionString =
            $"Data Source={DATABASE_HOST_NAME},1433;Initial Catalog=HappyCoding_TestingWithContainers;Integrated Security=SSPI;User Id=SA;Password=MySecret@Password123?;Trusted_Connection=False;Encrypt=False;";

        var solutionDirectory = TestUtil.GetSolutionDirectory();
        var applicationImage = new ImageFromDockerfileBuilder()
            .WithDockerfileDirectory(solutionDirectory)
            .WithDockerfile("HappyCoding.TestingWithContainers/Dockerfile")
            .WithBuildArgument("RESOURCE_REAPER_SESSION_ID", ResourceReaper.DefaultSessionId.ToString("D"))
            .Build();
        await applicationImage.CreateAsync();

        var applicationContainer = new ContainerBuilder()
            .WithImage(applicationImage)
            .WithNetwork(dockerNetwork)
            .WithPortBinding(80, true)
            .WithEnvironment("Kestrel__Endpoints__Http__Url", "http://+:80")
            .WithEnvironment("ASPNETCORE_URLS", "http://+:80")
            .WithEnvironment("ConnectionStrings__PersonDb", _sqlConnectionString)
            .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(80))
            .Build();

        // Start all containers
        await sqlEdgeContainer.StartAsync();
        await applicationContainer.StartAsync();

        _sqlConnectionStringFromPublic =
            $"Data Source=localhost,{sqlEdgeContainer.GetMappedPublicPort(1433)};Initial Catalog=HappyCoding_TestingWithContainers;Integrated Security=SSPI;User Id=SA;Password=MySecret@Password123?;Trusted_Connection=False;Encrypt=False;";

        var applicationPort = applicationContainer.GetMappedPublicPort(80);
        _applicationBaseUrl = $"http://localhost:{applicationPort}";

        _containers = new[] { sqlEdgeContainer, applicationContainer };
    }

    public async Task CleanupDatabaseAsync()
    {
        if (string.IsNullOrEmpty(_sqlConnectionStringFromPublic))
        {
            throw new InvalidOperationException("Container not started. Create a client before calling this method!");
        }

        await using var connection = new SqlConnection(_sqlConnectionStringFromPublic);
        await connection.OpenAsync();

        await using var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM dbo.Persons";
        await command.ExecuteNonQueryAsync();
    }
}