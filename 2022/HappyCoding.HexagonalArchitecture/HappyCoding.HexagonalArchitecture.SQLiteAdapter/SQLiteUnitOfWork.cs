using HappyCoding.HexagonalArchitecture.Domain.Ports;
using HappyCoding.HexagonalArchitecture.SQLiteAdapter.Bootstrap;
using HappyCoding.HexagonalArchitecture.SQLiteAdapter.Repositories;

namespace HappyCoding.HexagonalArchitecture.SQLiteAdapter;

internal class SQLiteUnitOfWork : IUnitOfWork
{
    private readonly AppDBContext _dbContext;
    
    public IWorkshopRepository Workshops { get; }

    public SQLiteUnitOfWork(AppDBContext dbContext, IDBBootstrap dbBootstrap)
    {
        _dbContext = dbContext;

        // Trigger for initial migration
        if (!dbBootstrap.BootstrapExecuted)
        {
            dbBootstrap.Bootstrap(dbContext);
        }

        this.Workshops = new SQLiteWorkshopRepository(_dbContext.Workshops);
    }
    
    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}