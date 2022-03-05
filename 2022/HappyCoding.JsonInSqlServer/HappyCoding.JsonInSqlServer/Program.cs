using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using HappyCoding.JsonInSqlServer.JsonModel;
using HappyCoding.JsonInSqlServer.Scenario1;
using HappyCoding.JsonInSqlServer.Scenario2;
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

            connectionStringBuilder.InitialCatalog = "JSON_IN_SQL__SCENARIO_1_A";
            await Scenario1Async(connectionStringBuilder.ConnectionString, false);
            Console.WriteLine();

            connectionStringBuilder.InitialCatalog = "JSON_IN_SQL__SCENARIO_1_B";
            await Scenario1Async(connectionStringBuilder.ConnectionString, true);
            Console.WriteLine();

            connectionStringBuilder.InitialCatalog = "JSON_IN_SQL__SCENARIO_2_A";
            await Scenario2Async(connectionStringBuilder.ConnectionString, false);
            Console.WriteLine();

            connectionStringBuilder.InitialCatalog = "JSON_IN_SQL__SCENARIO_2_B";
            await Scenario2Async(connectionStringBuilder.ConnectionString, true);
            Console.WriteLine();
        }

        /// <summary>
        /// Scenario 1: Store data as plain json in NVARCHAR(MAX) field.
        /// </summary>
        static async Task Scenario1Async(string connectionString, bool reducedPropertySize)
        {
            Console.WriteLine("####### Scenario 1 " + (reducedPropertySize ? "(reduced property size)" : ""));

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
            var random = new Random(1000);
            var charCount = (long) 0;
            var elapsed = await MeasureUtil.MeasureTimeAsync(10,  async () =>
            {
                for (var loopInner = 0; loopInner < 1000; loopInner++)
                {
                    await using var dbContext = new Scenario1DbContext(optionsBuilder.Options);

                    idCounter++;
                    var testDataRow = new ModelWithJsonData1(
                        $"ID-00000000000000000000{idCounter:D7}",
                        JsonRoot.CreateByRandom(random),
                        reducedPropertySize);
                    charCount += testDataRow.JsonData.Length;

                    await dbContext.TestingTable.AddAsync(testDataRow);
                    await dbContext.SaveChangesAsync();
                }
            });
            Console.WriteLine($"Write 1000 rows ({elapsed.TotalMilliseconds:F2} ms)");
            Console.WriteLine($"Average char count per json object: {charCount / idCounter} chars");

            // Read single row
            elapsed = await MeasureUtil.MeasureTimeAsync(10, async () =>
            {
                await using var dbContext = new Scenario1DbContext(optionsBuilder.Options);

                var indexToReed = random.Next(1000, 8000);
                var expectedKey = $"ID-00000000000000000000{indexToReed:D7}";

                var row = await dbContext.TestingTable
                    .Where(row => row.ID == expectedKey)
                    .FirstAsync();
                var json = row.GetJsonRoot();
            });
            Console.WriteLine($"Read single row with json ({elapsed.TotalMilliseconds:F2} ms)");

            // Update single row
            elapsed = await MeasureUtil.MeasureTimeAsync(10, async () =>
            {
                await using var dbContext = new Scenario1DbContext(optionsBuilder.Options);

                var indexToReed = random.Next(1000, 8000);
                var expectedKey = $"ID-00000000000000000000{indexToReed:D7}";

                var row = await dbContext.TestingTable
                    .Where(row => row.ID == expectedKey)
                    .FirstAsync();
                var json = row.GetJsonRoot();
                row.SetJsonRoot(JsonRoot.CreateByRandom(random));
            });
            Console.WriteLine($"Update single row with json ({elapsed.TotalMilliseconds:F2} ms)");
        }

        /// <summary>
        /// Scenario 2: Store data as plain json in NVARCHAR(MAX) field.
        /// </summary>
        static async Task Scenario2Async(string connectionString, bool reducedPropertySize)
        {
            Console.WriteLine("####### Scenario 2 " + (reducedPropertySize ? "(reduced property size)" : ""));

            await DBUtil.EnsureNewDBAsync(connectionString);

            var optionsBuilder = new DbContextOptionsBuilder<Scenario2DbContext>();
            optionsBuilder.UseSqlServer(connectionString);
            {
                await using var migrationContext = new Scenario2DbContext(optionsBuilder.Options);
                await migrationContext.Database.MigrateAsync();
            }
            Console.WriteLine("Database migrated");

            // Populate DB
            var idCounter = 0;
            var random = new Random(1000);
            var byteCount = (long) 0;
            var elapsed = await MeasureUtil.MeasureTimeAsync(10,  async () =>
            {
                for (var loopInner = 0; loopInner < 1000; loopInner++)
                {
                    await using var dbContext = new Scenario2DbContext(optionsBuilder.Options);

                    idCounter++;
                    var testDataRow = new ModelWithJsonData2(
                        $"ID-00000000000000000000{idCounter:D7}",
                        JsonRoot.CreateByRandom(random),
                        reducedPropertySize);
                    byteCount += testDataRow.JsonData.Length;

                    await dbContext.TestingTable.AddAsync(testDataRow);
                    await dbContext.SaveChangesAsync();
                }
            });
            Console.WriteLine($"Write 1000 rows ({elapsed.TotalMilliseconds:F2} ms)");
            Console.WriteLine($"Average byte count per json object: {byteCount / idCounter} bytes");

            // Read single row
            elapsed = await MeasureUtil.MeasureTimeAsync(10, async () =>
            {
                await using var dbContext = new Scenario2DbContext(optionsBuilder.Options);

                var indexToReed = random.Next(1000, 8000);
                var expectedKey = $"ID-00000000000000000000{indexToReed:D7}";

                var row = await dbContext.TestingTable
                    .Where(row => row.ID == expectedKey)
                    .FirstAsync();
                var json = row.GetJsonRoot();
            });
            Console.WriteLine($"Read single row with json ({elapsed.TotalMilliseconds:F2} ms)");

            // Update single row
            elapsed = await MeasureUtil.MeasureTimeAsync(10, async () =>
            {
                await using var dbContext = new Scenario2DbContext(optionsBuilder.Options);

                var indexToReed = random.Next(1000, 8000);
                var expectedKey = $"ID-00000000000000000000{indexToReed:D7}";

                var row = await dbContext.TestingTable
                    .Where(row => row.ID == expectedKey)
                    .FirstAsync();
                var json = row.GetJsonRoot();
                row.SetJsonRoot(JsonRoot.CreateByRandom(random));
                await dbContext.SaveChangesAsync();
            });
            Console.WriteLine($"Update single row with json ({elapsed.TotalMilliseconds:F2} ms)");
        }
    }
}
