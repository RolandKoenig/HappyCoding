using Microsoft.EntityFrameworkCore;

namespace HappyCoding.EFCoreOwnedTypes;
public class MyDbContextFactory
{
    public MyDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder =
            new DbContextOptionsBuilder<MyDbContext>();
        optionsBuilder.UseSqlServer();
        return new MyDbContext(optionsBuilder.Options);
    }
}
