using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using DotNet.Testcontainers.Images;
using Microsoft.Data.SqlClient;

namespace HappyCoding.TestingWithContainers.SystemTests.TestSetup;


public class TestEnvironmentFixture : IAsyncDisposable
{
    private IContainer[]? _containers;
    private IFutureDockerImage[]? _builtImages;

    private string? _sqlConnectionString;
    private string? _applicationBaseUrl;

    public string SqlConnectionString => _sqlConnectionString ?? string.Empty;

    public string ApplicationBaseUrl => _applicationBaseUrl ?? string.Empty;
    
    
    public async Task EnsureContainersLoadedAsync()
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
        if (OperatingSystem.IsMacOS()) { azureSqlTag = "";}
        var sqlEdgeContainer = new ContainerBuilder()
            .WithImage($"mcr.microsoft.com/azure-sql-edge{azureSqlTag}")
            .WithNetwork(dockerNetwork)
            .WithNetworkAliases(DATABASE_HOST_NAME)
            .WithEnvironment("ACCEPT_EULA", "Y")
            .WithEnvironment("MSSQL_USER", "SA")
            .WithEnvironment("MSSQL_SA_PASSWORD", "MySecret@Password123?")
            .WithEnvironment("MSSQL_PID", "Developer")
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
        _builtImages = new[] {applicationImage};

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

        var applicationPort = applicationContainer.GetMappedPublicPort(80);
        _applicationBaseUrl = $"http://localhost:{applicationPort}";

        _containers = new[] { sqlEdgeContainer, applicationContainer };
    }

    private async Task CleanupDatabaseAsync()
    {
        await using var connection = new SqlConnection(_sqlConnectionString);
        await connection.OpenAsync();

        await using var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM dbo.Persons";
        var countDeleted = await command.ExecuteNonQueryAsync();
    }

    /// <inheritdoc />
    public async ValueTask DisposeAsync()
    {
        if (_containers != null)
        {
            foreach (var actContainer in _containers)
            {
                await actContainer.DisposeAsync();
            }
            _containers = null;
        }

        if (_builtImages != null)
        {
            foreach (var actImage in _builtImages)
            {
                await actImage.DeleteAsync();
                await actImage.DisposeAsync();
            }
            _builtImages = null;
        }

        _sqlConnectionString = null;
    }
}

[CollectionDefinition(nameof(TestEnvironmentCollection))]
public class TestEnvironmentCollection : ICollectionFixture<TestEnvironmentFixture> { }
