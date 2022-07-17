namespace HappyCoding.HexagonalArchitecture.SQLiteAdapter.Bootstrap;

internal interface IDBBootstrap
{
    bool BootstrapExecuted { get; }
    
    void Bootstrap(AppDBContext dbContext);
}