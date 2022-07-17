using Microsoft.EntityFrameworkCore;

namespace HappyCoding.HexagonalArchitecture.SQLiteAdapter.Bootstrap;

internal class SQLiteDBBootstrap : IDBBootstrap
{
    private bool _migrateCalled;

    public bool BootstrapExecuted => _migrateCalled;

    public void Bootstrap(AppDBContext dbContext)
    {
        _migrateCalled = true;
        dbContext.Database.Migrate();
    }
}