using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HappyCoding.EFCoreQueryTagging
{
    public class TestingDBContextFactory : IDesignTimeDbContextFactory<TestingDBContext>
    {
        public TestingDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TestingDBContext>();
            optionsBuilder.UseSqlite();

            return new TestingDBContext(optionsBuilder.Options);
        }
    }
}
