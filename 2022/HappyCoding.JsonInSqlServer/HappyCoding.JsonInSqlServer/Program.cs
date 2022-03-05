using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using HappyCoding.JsonInSqlServer.JsonModel;
using HappyCoding.JsonInSqlServer.Scenario1;
using HappyCoding.JsonInSqlServer.Scenario2;
using HappyCoding.JsonInSqlServer.Scenario3;
using HappyCoding.JsonInSqlServer.Util;
using Microsoft.EntityFrameworkCore;

namespace HappyCoding.JsonInSqlServer
{
    internal class Program
    {
        // Parameter
        private const int RANDOM_SEED = 123;
        private const int NEW_ENTRIES_PER_CYCLE = 1000;
        private const int TIME_MEASURE_CYCLE_COUNT = 10;

        static async Task Main(string[] args)
        {
            Console.WriteLine("Parameter");
            Console.WriteLine($" - {nameof(RANDOM_SEED)} = {RANDOM_SEED}");
            Console.WriteLine($" - {nameof(NEW_ENTRIES_PER_CYCLE)} = {NEW_ENTRIES_PER_CYCLE}");
            Console.WriteLine($" - {nameof(TIME_MEASURE_CYCLE_COUNT)} = {TIME_MEASURE_CYCLE_COUNT}");
            Console.WriteLine();

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

            connectionStringBuilder.InitialCatalog = "JSON_IN_SQL__SCENARIO_3";
            await Scenario3Async(connectionStringBuilder.ConnectionString, false);
            Console.WriteLine();
        }

        /// <summary>
        /// Scenario 1: Store data as plain json in NVARCHAR(MAX) field.
        /// </summary>
        static async Task Scenario1Async(string connectionString, bool reducedPropertySize)
        {
            Console.WriteLine("####### Scenario 1 - raw, NVARCHAR(MAX), JsonDotNet " + (reducedPropertySize ? "(reduced property size)" : ""));

            await DBUtil.EnsureNewDBAsync(connectionString);

            var optionsBuilder = new DbContextOptionsBuilder<Scenario1DbContext>();
            optionsBuilder.UseSqlServer(connectionString);
            {
                await using var migrationContext = new Scenario1DbContext(optionsBuilder.Options);
                await migrationContext.Database.MigrateAsync();
            }
            Console.WriteLine("Database migrated");

            // Populate DB
            Console.WriteLine("Populate DB...");
            var idCounter = 0;
            var random = new Random(RANDOM_SEED);
            var charCount = (long) 0;
            var elapsed = await MeasureUtil.MeasureTimeAsync(TIME_MEASURE_CYCLE_COUNT,  async () =>
            {
                for (var loopInner = 0; loopInner < NEW_ENTRIES_PER_CYCLE; loopInner++)
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
            Console.WriteLine($" - Write {NEW_ENTRIES_PER_CYCLE} rows ({elapsed.TotalMilliseconds:F2} ms)");
            Console.WriteLine($" - Average char count per json object: {charCount / idCounter} chars");

            // Read single row
            Console.WriteLine("Read single row");
            elapsed = await MeasureUtil.MeasureTimeAsync(TIME_MEASURE_CYCLE_COUNT, async () =>
            {
                await using var dbContext = new Scenario1DbContext(optionsBuilder.Options);

                var indexToReed = random.Next(0, NEW_ENTRIES_PER_CYCLE * TIME_MEASURE_CYCLE_COUNT);
                var expectedKey = $"ID-00000000000000000000{indexToReed:D7}";

                var row = await dbContext.TestingTable
                    .Where(row => row.ID == expectedKey)
                    .FirstAsync();
                var json = row.GetJsonRoot();
            });
            Console.WriteLine($" - Read single row with json ({elapsed.TotalMilliseconds:F2} ms)");

            // Update single row
            Console.WriteLine("Update single row");
            elapsed = await MeasureUtil.MeasureTimeAsync(TIME_MEASURE_CYCLE_COUNT, async () =>
            {
                await using var dbContext = new Scenario1DbContext(optionsBuilder.Options);

                var indexToReed = random.Next(0, NEW_ENTRIES_PER_CYCLE * TIME_MEASURE_CYCLE_COUNT);
                var expectedKey = $"ID-00000000000000000000{indexToReed:D7}";

                var row = await dbContext.TestingTable
                    .Where(row => row.ID == expectedKey)
                    .FirstAsync();
                var json = row.GetJsonRoot();
                row.SetJsonRoot(JsonRoot.CreateByRandom(random));
            });
            Console.WriteLine($" - Update single row with json ({elapsed.TotalMilliseconds:F2} ms)");
        }

        /// <summary>
        /// Scenario 2: Store data as compressed json in VARBINARY(MAX) field.
        /// </summary>
        static async Task Scenario2Async(string connectionString, bool reducedPropertySize)
        {
            Console.WriteLine("####### Scenario 2 compressed, VARBINARY(MAX), JsonDotNet " + (reducedPropertySize ? "(reduced property size)" : ""));

            await DBUtil.EnsureNewDBAsync(connectionString);

            var optionsBuilder = new DbContextOptionsBuilder<Scenario2DbContext>();
            optionsBuilder.UseSqlServer(connectionString);
            {
                await using var migrationContext = new Scenario2DbContext(optionsBuilder.Options);
                await migrationContext.Database.MigrateAsync();
            }
            Console.WriteLine("Database migrated");

            // Populate DB
            Console.WriteLine("Populate DB...");
            var idCounter = 0;
            var random = new Random(RANDOM_SEED);
            var byteCount = (long) 0;
            var elapsed = await MeasureUtil.MeasureTimeAsync(TIME_MEASURE_CYCLE_COUNT,  async () =>
            {
                for (var loopInner = 0; loopInner < NEW_ENTRIES_PER_CYCLE; loopInner++)
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
            Console.WriteLine($" - Write {NEW_ENTRIES_PER_CYCLE} rows ({elapsed.TotalMilliseconds:F2} ms)");
            Console.WriteLine($" - Average byte count per json object: {byteCount / idCounter} bytes");

            // Read single row
            Console.WriteLine("Read single row");
            elapsed = await MeasureUtil.MeasureTimeAsync(TIME_MEASURE_CYCLE_COUNT, async () =>
            {
                await using var dbContext = new Scenario2DbContext(optionsBuilder.Options);

                var indexToReed = random.Next(0, NEW_ENTRIES_PER_CYCLE * TIME_MEASURE_CYCLE_COUNT);
                var expectedKey = $"ID-00000000000000000000{indexToReed:D7}";

                var row = await dbContext.TestingTable
                    .Where(row => row.ID == expectedKey)
                    .FirstAsync();
                var json = row.GetJsonRoot();
            });
            Console.WriteLine($" - Read single row with json ({elapsed.TotalMilliseconds:F2} ms)");

            // Update single row
            Console.WriteLine("Update single row");
            elapsed = await MeasureUtil.MeasureTimeAsync(TIME_MEASURE_CYCLE_COUNT, async () =>
            {
                await using var dbContext = new Scenario2DbContext(optionsBuilder.Options);

                var indexToReed = random.Next(0, NEW_ENTRIES_PER_CYCLE * TIME_MEASURE_CYCLE_COUNT);
                var expectedKey = $"ID-00000000000000000000{indexToReed:D7}";

                var row = await dbContext.TestingTable
                    .Where(row => row.ID == expectedKey)
                    .FirstAsync();
                var json = row.GetJsonRoot();
                row.SetJsonRoot(JsonRoot.CreateByRandom(random));
                await dbContext.SaveChangesAsync();
            });
            Console.WriteLine($" - Update single row with json ({elapsed.TotalMilliseconds:F2} ms)");
        }

        /// <summary>
        /// Scenario 31: Store data as plain json in NVARCHAR(MAX) field. Use System.Text.Json for serialization / deserialization.
        /// </summary>
        static async Task Scenario3Async(string connectionString, bool reducedPropertySize)
        {
            Console.WriteLine("####### Scenario 3 compressed, VARBINARY(MAX), System.Text.Json " + (reducedPropertySize ? "(reduced property size)" : ""));

            await DBUtil.EnsureNewDBAsync(connectionString);

            var optionsBuilder = new DbContextOptionsBuilder<Scenario3DbContext>();
            optionsBuilder.UseSqlServer(connectionString);
            {
                await using var migrationContext = new Scenario3DbContext(optionsBuilder.Options);
                await migrationContext.Database.MigrateAsync();
            }
            Console.WriteLine("Database migrated");

            // Populate DB
            Console.WriteLine("Populate DB...");
            var idCounter = 0;
            var random = new Random(RANDOM_SEED);
            var charCount = (long) 0;
            var elapsed = await MeasureUtil.MeasureTimeAsync(TIME_MEASURE_CYCLE_COUNT,  async () =>
            {
                for (var loopInner = 0; loopInner < NEW_ENTRIES_PER_CYCLE; loopInner++)
                {
                    await using var dbContext = new Scenario3DbContext(optionsBuilder.Options);

                    idCounter++;
                    var testDataRow = new ModelWithJsonData3(
                        $"ID-00000000000000000000{idCounter:D7}",
                        JsonRoot.CreateByRandom(random),
                        reducedPropertySize);
                    charCount += testDataRow.JsonData.Length;

                    await dbContext.TestingTable.AddAsync(testDataRow);
                    await dbContext.SaveChangesAsync();
                }
            });
            Console.WriteLine($" - Write {NEW_ENTRIES_PER_CYCLE} rows ({elapsed.TotalMilliseconds:F2} ms)");
            Console.WriteLine($" - Average char count per json object: {charCount / idCounter} chars");

            // Read single row
            Console.WriteLine("Read single row");
            elapsed = await MeasureUtil.MeasureTimeAsync(TIME_MEASURE_CYCLE_COUNT, async () =>
            {
                await using var dbContext = new Scenario3DbContext(optionsBuilder.Options);

                var indexToReed = random.Next(0, NEW_ENTRIES_PER_CYCLE * TIME_MEASURE_CYCLE_COUNT);
                var expectedKey = $"ID-00000000000000000000{indexToReed:D7}";

                var row = await dbContext.TestingTable
                    .Where(row => row.ID == expectedKey)
                    .FirstAsync();
                var json = row.GetJsonRoot();
            });
            Console.WriteLine($" - Read single row with json ({elapsed.TotalMilliseconds:F2} ms)");

            // Update single row
            Console.WriteLine("Update single row");
            elapsed = await MeasureUtil.MeasureTimeAsync(TIME_MEASURE_CYCLE_COUNT, async () =>
            {
                await using var dbContext = new Scenario3DbContext(optionsBuilder.Options);

                var indexToReed = random.Next(0, NEW_ENTRIES_PER_CYCLE * TIME_MEASURE_CYCLE_COUNT);
                var expectedKey = $"ID-00000000000000000000{indexToReed:D7}";

                var row = await dbContext.TestingTable
                    .Where(row => row.ID == expectedKey)
                    .FirstAsync();
                var json = row.GetJsonRoot();
                row.SetJsonRoot(JsonRoot.CreateByRandom(random));
            });
            Console.WriteLine($" - Update single row with json ({elapsed.TotalMilliseconds:F2} ms)");
        }
    }
}
