using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HappyCoding.JsonInSqlServer.Scenario3
{
    public class Scenario2DbContextFactory : IDesignTimeDbContextFactory<Scenario3DbContext>
    {
        public Scenario3DbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<Scenario3DbContext>();
            optionsBuilder.UseSqlServer();

            return new Scenario3DbContext(optionsBuilder.Options);
        }
    }
}
