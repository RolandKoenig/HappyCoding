using HappyCoding.HexagonalArchitecture.Domain.Ports;
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
                options => options.UseSqlite(dbConnectionString));
    }
}