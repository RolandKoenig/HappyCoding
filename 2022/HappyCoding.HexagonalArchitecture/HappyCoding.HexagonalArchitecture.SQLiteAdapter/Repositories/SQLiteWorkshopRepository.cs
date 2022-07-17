using HappyCoding.HexagonalArchitecture.Domain.Model;
using HappyCoding.HexagonalArchitecture.Domain.Ports;
using Microsoft.EntityFrameworkCore;

namespace HappyCoding.HexagonalArchitecture.SQLiteAdapter.Repositories;

public class SQLiteWorkshopRepository : IWorkshopRepository
{
    private readonly DbSet<Workshop> _dbWorkshops;
    
    internal SQLiteWorkshopRepository(DbSet<Workshop> dbWorkshops)
    {
        _dbWorkshops = dbWorkshops;
    }
    
    public Task AddWorkshopAsync(Workshop workshop, CancellationToken cancellationToken)
    {
        var result =_dbWorkshops.AddAsync(workshop, cancellationToken);
        if (result.IsCompletedSuccessfully)
        {
            return Task.CompletedTask;
        }
        else
        {
            return result.AsTask();
        }
    }

    public Task DeleteWorkshopAsync(Workshop workshop, CancellationToken cancellationToken)
    {
        _dbWorkshops.Remove(workshop);
        return Task.CompletedTask;
    }
}