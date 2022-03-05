using Microsoft.EntityFrameworkCore;

namespace HappyCoding.JsonInSqlServer.Scenario3
{
    public class Scenario3DbContext : DbContext
    {
        private string _connectionString;

        public DbSet<ModelWithJsonData3> TestingTable { get; set; }

        public Scenario3DbContext(DbContextOptions options)
            : base(options)
        {

        }
    }
}
