using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using HappyCoding.EFIncludePerformance.Model;
using HappyCoding.EFIncludePerformance.Util;
using Microsoft.EntityFrameworkCore;
using Process = HappyCoding.EFIncludePerformance.Model.Process;

namespace HappyCoding.EFIncludePerformance
{
    internal class Program
    {
        // Parameters
        private const string CONNECTION_STRING = "Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=HappyCoding_2022_EFIncludePerformance;Integrated Security=SSPI";
        private const int RANDOM_SEED = 100;
        private const int COUNT_PROCESSES = 1000;
        private const int COUNT_ACTIVITIES_PER_PROCESS = 2;
        private const int COUNT_RUNS_PER_EXPERIMENT = 10;

        static async Task Main(string[] args)
        {
            var random = new Random(100);

            var dbOptions = await CreateAndMigrateDatabaseAsync();

            await PopulateDatabaseAsync(dbOptions, random);

            // Warmup
            await TestRandomGetByIncludeAsync(dbOptions, random, true);
            await TestRandomGetByIncludeAsSplitQueryAsync(dbOptions, random, true);
            await TestRandomGetByLoadSeparatelyAsync(dbOptions, random, true);

            // Check include
            await TestRandomGetByIncludeAsync(dbOptions, random);
            await TestRandomGetByIncludeAsync(dbOptions, random);
            await TestRandomGetByIncludeAsync(dbOptions, random);
            await TestRandomGetByIncludeAsync(dbOptions, random);

            // Check include with split query
            await TestRandomGetByIncludeAsSplitQueryAsync(dbOptions, random);
            await TestRandomGetByIncludeAsSplitQueryAsync(dbOptions, random);
            await TestRandomGetByIncludeAsSplitQueryAsync(dbOptions, random);
            await TestRandomGetByIncludeAsSplitQueryAsync(dbOptions, random);

            // Check separate load call
            await TestRandomGetByLoadSeparatelyAsync(dbOptions, random);
            await TestRandomGetByLoadSeparatelyAsync(dbOptions, random);
            await TestRandomGetByLoadSeparatelyAsync(dbOptions, random);
            await TestRandomGetByLoadSeparatelyAsync(dbOptions, random);
        }

        private static async Task<DbContextOptions<TestingDBContext>> CreateAndMigrateDatabaseAsync()
        {
            Console.WriteLine("Create database...");
            await DBUtil.EnsureNewDBAsync(CONNECTION_STRING);

            Console.WriteLine("Migrate database...");
            var optionsBuilder = new DbContextOptionsBuilder<TestingDBContext>();
            optionsBuilder.UseSqlServer(CONNECTION_STRING);
            {
                await using var migrationContext = new TestingDBContext(optionsBuilder.Options);
                await migrationContext.Database.MigrateAsync();
            }
            Console.WriteLine("Database migrated");

            return optionsBuilder.Options;
        }

        private static async Task PopulateDatabaseAsync(DbContextOptions<TestingDBContext> dbContextOptions, Random random)
        {
            await using var dbContext = new TestingDBContext(dbContextOptions);

            Console.WriteLine("Populate DB...");
            for (var processNumber = 0; processNumber < COUNT_PROCESSES; processNumber++)
            {
                var processKey = GetProcessKey(processNumber);

                var newProcess = new Process()
                {
                    ID = processKey,
                    CreateTimestamp = DateTimeOffset.UtcNow,
                    Activities = new List<ProcessActivity>(),
                    Field1 = GetRandomInt(random),
                    Field2 = GetRandomInt(random),
                    Field3 = GetRandomInt(random),
                    Field4 = GetRandomString(random),
                    Field5 = GetRandomString(random),
                    Field6 = GetRandomInt(random)
                };

                for (var loopActivity = 0; loopActivity < COUNT_ACTIVITIES_PER_PROCESS; loopActivity++)
                {
                    newProcess.Activities.Add(new ProcessActivity()
                    {
                        ProcessID = processKey,
                        Field1 = GetRandomInt(random),
                        Field2 = GetRandomInt(random),
                        Field3 = GetRandomInt(random),
                        Field4 = GetRandomString(random),
                        Field5 = GetRandomString(random),
                        Field6 = GetRandomInt(random),
                        Field7 = GetRandomBinary(random)
                    });
                }

                await dbContext.Processes.AddAsync(newProcess);
            }
            Console.WriteLine("DB populated");

            await dbContext.SaveChangesAsync();
        }

        private static async Task TestRandomGetByIncludeAsync(
            DbContextOptions<TestingDBContext> dbContextOptions,
            Random random,
            bool discardOutput = false)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            for (var loop = 0; loop < COUNT_RUNS_PER_EXPERIMENT; loop++)
            {
                var processNumber = random.Next(0, COUNT_PROCESSES);
                var processKey = GetProcessKey(processNumber);

                await using var dbContext = new TestingDBContext(dbContextOptions);

                var process = await dbContext.Processes
                    .Where(x => x.ID == processKey)
                    .Include(x => x.Activities)
                    .ToArrayAsync();
            }
            stopWatch.Stop();

            if (!discardOutput)
            {
                var totalMilliseconds = stopWatch.Elapsed.TotalMilliseconds;
                var millisecondsPerRun = totalMilliseconds / COUNT_RUNS_PER_EXPERIMENT;
                Console.WriteLine(
                    $"{nameof(TestRandomGetByIncludeAsync)}: " +
                    $"{totalMilliseconds:N2} ms total, " +
                    $"{millisecondsPerRun:N2} ms per run");
            }
        }

        private static async Task TestRandomGetByIncludeAsSplitQueryAsync(
            DbContextOptions<TestingDBContext> dbContextOptions,
            Random random,
            bool discardOutput = false)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            for (var loop = 0; loop < COUNT_RUNS_PER_EXPERIMENT; loop++)
            {
                var processNumber = random.Next(0, COUNT_PROCESSES);
                var processKey = GetProcessKey(processNumber);

                await using var dbContext = new TestingDBContext(dbContextOptions);

                var process = await dbContext.Processes
                    .Where(x => x.ID == processKey)
                    .Include(x => x.Activities)
                    .AsSplitQuery()
                    .ToArrayAsync();
            }
            stopWatch.Stop();

            if (!discardOutput)
            {
                var totalMilliseconds = stopWatch.Elapsed.TotalMilliseconds;
                var millisecondsPerRun = totalMilliseconds / COUNT_RUNS_PER_EXPERIMENT;
                Console.WriteLine(
                    $"{nameof(TestRandomGetByIncludeAsSplitQueryAsync)}: " +
                    $"{totalMilliseconds:N2} ms total, " +
                    $"{millisecondsPerRun:N2} ms per run");
            }
        }

        private static async Task TestRandomGetByLoadSeparatelyAsync(
            DbContextOptions<TestingDBContext> dbContextOptions,
            Random random,
            bool discardOutput = false)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            for (var loop = 0; loop < COUNT_RUNS_PER_EXPERIMENT; loop++)
            {
                var processNumber = random.Next(0, COUNT_PROCESSES);
                var processKey = GetProcessKey(processNumber);

                await using var dbContext = new TestingDBContext(dbContextOptions);

                var process = await dbContext.Processes
                    .Where(x => x.ID == processKey)
                    .ToArrayAsync();

                await dbContext.ProcessActivities
                    .Where(x => x.ProcessID == processKey)
                    .LoadAsync();
            }
            stopWatch.Stop();

            if (!discardOutput)
            {            
                var totalMilliseconds = stopWatch.Elapsed.TotalMilliseconds;
                var millisecondsPerRun = totalMilliseconds / COUNT_RUNS_PER_EXPERIMENT;
                Console.WriteLine(
                    $"{nameof(TestRandomGetByLoadSeparatelyAsync)}: " +
                    $"{totalMilliseconds:N2} ms total, " +
                    $"{millisecondsPerRun:N2} ms per run");
            }
        }

        private static string GetProcessKey(int processNumber)
        {
            return "P" + processNumber.ToString("D7") + "1234" + processNumber.ToString("D7") + "12345678901";
        }

        private static int GetRandomInt(Random random)
        {
            return random.Next(0, 100000);
        }

        private static string GetRandomString(Random random)
        {
            return random.Next(0, 100000).ToString("D10");
        }

        private static byte[] GetRandomBinary(Random random)
        {
            var result = new byte[random.Next(2000, 15000)];
            for (var loop = 0; loop < result.Length; loop++)
            {
                result[loop] = (byte)random.Next(0, 255);
            }
            return result;
        }
    }
}
