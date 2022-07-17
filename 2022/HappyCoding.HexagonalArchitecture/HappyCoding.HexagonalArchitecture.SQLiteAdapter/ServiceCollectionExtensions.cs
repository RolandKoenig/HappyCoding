using System.Data.Common;
using HappyCoding.HexagonalArchitecture.Domain.Ports;
using HappyCoding.HexagonalArchitecture.SQLiteAdapter.Bootstrap;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HappyCoding.HexagonalArchitecture.SQLiteAdapter;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSQLiteAdapter(
        this IServiceCollection serviceCollection,
        string dbConnectionString)
    {
        return serviceCollection
            .AddScoped<IUnitOfWork, SQLiteUnitOfWork>()
            .AddDbContext<AppDBContext>(
                options => options.UseSqlite(dbConnectionString))
            .AddSingleton<IDBBootstrap, SQLiteDBBootstrap>();
    }
    
    public static IServiceCollection AddSQLiteAdapter(
        this IServiceCollection serviceCollection,
        DbConnection dbConnection)
    {
        return serviceCollection
            .AddScoped<IUnitOfWork, SQLiteUnitOfWork>()
            .AddDbContext<AppDBContext>(
                options => options.UseSqlite(dbConnection))
            .AddSingleton<IDBBootstrap, SQLiteDBBootstrap>();
    }
}