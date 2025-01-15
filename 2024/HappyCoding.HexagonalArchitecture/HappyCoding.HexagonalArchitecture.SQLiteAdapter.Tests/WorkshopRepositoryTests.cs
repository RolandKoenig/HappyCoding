using System.Data.Common;
using HappyCoding.HexagonalArchitecture.Domain.Model;
using HappyCoding.HexagonalArchitecture.Domain.Ports;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;

namespace HappyCoding.HexagonalArchitecture.SQLiteAdapter.Tests;

public class WorkshopRepositoryTests : IDisposable
{
    private readonly DbConnection _dbConnection;
    private readonly IServiceProvider _serviceProvider;
    
    public WorkshopRepositoryTests()
    {
        _dbConnection = new SqliteConnection("Filename=:memory:");
        _dbConnection.Open();
        
        var services = new ServiceCollection();
        services.AddSQLiteAdapter(_dbConnection);

        _serviceProvider = services.BuildServiceProvider();
    }
    
    public void Dispose()
    {
        ((IDisposable)_serviceProvider).Dispose();
        _dbConnection.Dispose();
    }
    
    [Fact]
    public async Task CreateWorkshop()
    {
        // Arrange
        await using var serviceScope = _serviceProvider.CreateAsyncScope();
        var unitOfWork = serviceScope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        
        // Act
        var createdWorkshop = Workshop.CreateNew(
            "Test", "Test",
            DateTimeOffset.UtcNow, 
            Array.Empty<ProtocolEntry>());
        await unitOfWork.Workshops.AddWorkshopAsync(
            createdWorkshop,
            CancellationToken.None);
        await unitOfWork.SaveChangesAsync(CancellationToken.None);
        
        // Assert
        await using var assertScope = _serviceProvider.CreateAsyncScope();
        var assertUnitOfWork = assertScope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        var queriedWorkshop = await assertUnitOfWork.Workshops.GetWorkshopAsync(
            createdWorkshop.ID,
            CancellationToken.None);
    }
}