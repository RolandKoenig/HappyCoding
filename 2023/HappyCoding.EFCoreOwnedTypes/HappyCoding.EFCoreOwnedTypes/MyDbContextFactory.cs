using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HappyCoding.EFCoreOwnedTypes;
public class MyDbContextFactory : IDesignTimeDbContextFactory<MyDbContext>
{
    public MyDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder =
            new DbContextOptionsBuilder<MyDbContext>();
        optionsBuilder.UseSqlServer();
        return new MyDbContext(optionsBuilder.Options);
    }
}
