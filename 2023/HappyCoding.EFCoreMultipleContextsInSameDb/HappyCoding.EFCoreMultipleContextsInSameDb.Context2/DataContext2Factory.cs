using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HappyCoding.EFCoreMultipleContextsInSameDb.Context2;

internal class DataContext2Factory : IDesignTimeDbContextFactory<DataContext2>
{
    public DataContext2 CreateDbContext(string[] args)
    {
        var optionsBuilder =
            new DbContextOptionsBuilder<DataContext2>();
        optionsBuilder.UseSqlServer();
        return new DataContext2(optionsBuilder.Options);
    }
}
