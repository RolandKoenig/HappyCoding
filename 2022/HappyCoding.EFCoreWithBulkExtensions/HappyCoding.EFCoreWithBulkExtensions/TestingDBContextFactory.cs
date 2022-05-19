using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HappyCoding.EFCoreWithBulkExtensions
{
    internal class TestingDBContextFactory : IDesignTimeDbContextFactory<TestingDBContext>
    {
        public TestingDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TestingDBContext>();
            optionsBuilder.UseSqlServer();

            return new TestingDBContext(optionsBuilder.Options);
        }
    }
}
