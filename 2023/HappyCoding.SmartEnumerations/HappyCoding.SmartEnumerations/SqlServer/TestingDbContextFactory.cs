using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HappyCoding.SmartEnumerations.SqlServer;

public class TestingDbContextFactory : IDesignTimeDbContextFactory<TestingDbContext>
{
    public TestingDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder =
            new DbContextOptionsBuilder<TestingDbContext>();
        optionsBuilder.UseSqlServer();
        return new TestingDbContext(optionsBuilder.Options);
    }
}
