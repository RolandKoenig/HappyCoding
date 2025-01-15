using System.Collections.Immutable;
using HappyCoding.HexagonalArchitecture.Domain.Model;
using HappyCoding.HexagonalArchitecture.Domain.Model.Projections;
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

    public Task<Workshop> GetWorkshopAsync(Guid workshopID, CancellationToken cancellationToken)
    {
        return _dbWorkshops
            .Where(x => x.ID == workshopID)
            .Include(x => x.Protocol)
            .FirstAsync(cancellationToken);
    }

    public Task<Workshop?> TryGetWorkshopAsync(Guid workshopID, CancellationToken cancellationToken)
    {
        return _dbWorkshops
            .Where(x => x.ID == workshopID)
            .Include(x => x.Protocol)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<ImmutableArray<WorkshopShortInfo>> SearchWorkshopsAsync(string queryString, CancellationToken cancellationToken)
    {
        var queryable = (IQueryable<Workshop>)_dbWorkshops;

        if (!string.IsNullOrEmpty(queryString))
        {
            queryable = queryable.Where(x =>
                (x.Project.ToLower() == queryString.ToLower()) ||
                (x.Title.ToLower() == queryString.ToLower()));
        }

        var result = await queryable
            .Select(x => new WorkshopShortInfo()
            {
                ID = x.ID,
                Project = x.Project,
                StartTimestamp = x.StartTimestamp,
                Title = x.Title
            })
            .ToArrayAsync(cancellationToken);
        return ImmutableArray.Create(result);
    }

    public async Task DeleteWorkshopAsync(Guid workshopID, CancellationToken cancellationToken)
    {
        var workshop = await _dbWorkshops
            .Where(x => x.ID == workshopID)
            .FirstAsync(cancellationToken);
        _dbWorkshops.Remove(workshop);
    }
}