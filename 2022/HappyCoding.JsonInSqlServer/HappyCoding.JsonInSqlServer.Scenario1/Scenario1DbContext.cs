using Microsoft.EntityFrameworkCore;

namespace HappyCoding.JsonInSqlServer.Scenario1
{
    public class Scenario1DbContext : DbContext
    {
        public DbSet<ModelWithJsonData1> TestingTable { get; set; }

        public Scenario1DbContext(DbContextOptions options)
            : base(options)
        {

        }
    }
}
