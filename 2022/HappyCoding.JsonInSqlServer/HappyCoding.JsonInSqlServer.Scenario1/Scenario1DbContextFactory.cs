using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HappyCoding.JsonInSqlServer.Scenario1
{
    public class Scenario1DbContextFactory : IDesignTimeDbContextFactory<Scenario1DbContext>
    {
        public Scenario1DbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<Scenario1DbContext>();
            optionsBuilder.UseSqlServer();

            return new Scenario1DbContext(optionsBuilder.Options);
        }
    }
}
