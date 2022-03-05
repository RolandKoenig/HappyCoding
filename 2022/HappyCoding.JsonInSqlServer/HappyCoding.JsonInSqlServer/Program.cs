using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using HappyCoding.JsonInSqlServer.Scenario1;
using HappyCoding.JsonInSqlServer.Util;
using Microsoft.EntityFrameworkCore;

namespace HappyCoding.JsonInSqlServer
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // Connection string
            var connectionStringTemplate =
                "Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=Template;Integrated Security=SSPI";
            var connectionStringBuilder = new SqlConnectionStringBuilder(connectionStringTemplate);


            connectionStringBuilder.InitialCatalog = "JSON_IN_SQL__SCENARIO_1";
            await Scenario1Async(connectionStringBuilder.ConnectionString);
        }

        static async Task Scenario1Async(string connectionString)
        {
            Console.WriteLine("####### Scenario 1");

            await DBUtil.EnsureNewDBAsync(connectionString);

            var optionsBuilder = new DbContextOptionsBuilder<Scenario1DbContext>();
            optionsBuilder.UseSqlServer(connectionString);
            {
                await using var migrationContext = new Scenario1DbContext(optionsBuilder.Options);
                await migrationContext.Database.MigrateAsync();
            }
            Console.WriteLine("Database migrated");

            // Populate DB
            var idCounter = 0;
            var elapsed = await MeasureUtil.MeasureTimeAsync(10,  async () =>
            {
                for (var loopInner = 0; loopInner < 1000; loopInner++)
                {
                    await using var dbContext = new Scenario1DbContext(optionsBuilder.Options);

                    idCounter++;
                    var testDataRow = new ModelWithJsonData($"ID-00000000000000000000{idCounter:D7}");

                    await dbContext.TestingTable.AddAsync(testDataRow);
                    await dbContext.SaveChangesAsync();
                }
            });
            Console.WriteLine($"Write 1000 rows ({elapsed.TotalMilliseconds:F2} ms)");

        }
    }
}
