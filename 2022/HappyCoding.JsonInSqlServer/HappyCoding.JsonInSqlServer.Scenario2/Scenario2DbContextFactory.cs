using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HappyCoding.JsonInSqlServer.Scenario2
{
    public class Scenario2DbContextFactory : IDesignTimeDbContextFactory<Scenario2DbContext>
    {
        public Scenario2DbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<Scenario2DbContext>();
            optionsBuilder.UseSqlServer();

            return new Scenario2DbContext(optionsBuilder.Options);
        }
    }
}
