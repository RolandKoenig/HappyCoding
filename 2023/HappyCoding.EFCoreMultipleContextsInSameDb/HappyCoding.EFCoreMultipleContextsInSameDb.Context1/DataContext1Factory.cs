using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HappyCoding.EFCoreMultipleContextsInSameDb.Context1;

internal class DataContext1Factory : IDesignTimeDbContextFactory<DataContext1>
{
    public DataContext1 CreateDbContext(string[] args)
    {
        var optionsBuilder =
            new DbContextOptionsBuilder<DataContext1>();
        optionsBuilder.UseSqlServer();
        return new DataContext1(optionsBuilder.Options);
    }
}
