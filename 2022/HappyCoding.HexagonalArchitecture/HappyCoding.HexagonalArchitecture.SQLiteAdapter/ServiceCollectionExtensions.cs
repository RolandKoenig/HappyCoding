using Microsoft.Extensions.DependencyInjection;

namespace HappyCoding.HexagonalArchitecture.SQLiteAdapter;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSQLiteAdapter(this IServiceCollection serviceCollection)
    {

        return serviceCollection;
    }
}