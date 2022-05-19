using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using EFCore.BulkExtensions;
using HappyCoding.EFCoreWithBulkExtensions.Model;
using HappyCoding.EFCoreWithBulkExtensions.Util;
using Microsoft.EntityFrameworkCore;

namespace HappyCoding.EFCoreWithBulkExtensions
{
    internal class Program
    {
        private const string CONNECTION_STRING = "Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=HappyCoding_2022_EFCoreWithBulkExtensions;Integrated Security=SSPI";
        private const int RANDOM_SEED = 100;
        private const int POSITION_COUNT = 10000;
        private const int CYCLE_COUNT = 3;

        static async Task Main(string[] args)
        {
            // Create database
            Console.WriteLine("Create database");
            await DBUtil.EnsureNewDBAsync(CONNECTION_STRING);

            // Migrate database
            Console.WriteLine("Migrate database");
            var optionsBuilder = new DbContextOptionsBuilder<TestingDBContext>();
            optionsBuilder.UseSqlServer(CONNECTION_STRING);
            {
                await using var migrationContext = new TestingDBContext(optionsBuilder.Options);
                await migrationContext.Database.MigrateAsync();
            }
            Console.WriteLine("Database migrated");

            // Initial filling
            var random = new Random(RANDOM_SEED);

            // Warmup
            await CreateNewOrder_DefaultAsync(optionsBuilder.Options, Guid.NewGuid(), 10, random);
            await CreateNewOrder_BulkAsync(optionsBuilder.Options, Guid.NewGuid(), 10, random);

            // Normal inserts
            for (var loop = 0; loop < CYCLE_COUNT; loop++)
            {
                Console.WriteLine($"Normal Insert {loop + 1}");
            
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                await CreateNewOrder_DefaultAsync(optionsBuilder.Options, Guid.NewGuid(), POSITION_COUNT, random);
                stopwatch.Stop();
            
                Console.WriteLine($"took {stopwatch.Elapsed.TotalMilliseconds:N2} ms");
                Console.WriteLine();
            }

            // Bulk inserts
            for (var loop = 0; loop < CYCLE_COUNT; loop++)
            {
                Console.WriteLine($"Bulk Insert {loop + 1}");

                var stopwatch = new Stopwatch();
                stopwatch.Start();
                await CreateNewOrder_BulkAsync(optionsBuilder.Options, Guid.NewGuid(), POSITION_COUNT, random);
                stopwatch.Stop();

                Console.WriteLine($"took {stopwatch.Elapsed.TotalMilliseconds:N2} ms");
                Console.WriteLine();
            }

            Console.WriteLine("Finished");
            Console.ReadLine();
        }

        private static async Task CreateNewOrder_DefaultAsync(DbContextOptions<TestingDBContext> dbContextOptions, Guid orderID, int countPositions, Random random)
        {
            await using var dbContext = new TestingDBContext(dbContextOptions);

            var order = new Order(orderID, DateTimeOffset.UtcNow);
            for(var loop=0; loop<countPositions; loop++)
            {
                var newPosition = new OrderPosition(
                    order,
                    loop + 1,
                    CreateRandomString(random, 13),
                    random.NextDouble() * 10.0);
                order.Positions.Add(newPosition);
            }
            dbContext.Orders.Add(order);

            await dbContext.SaveChangesAsync();
        }

        private static async Task CreateNewOrder_BulkAsync(DbContextOptions<TestingDBContext> dbContextOptions, Guid orderID, int countPositions, Random random)
        {
            await using var dbContext = new TestingDBContext(dbContextOptions);

            var order = new Order(orderID, DateTimeOffset.UtcNow);
            dbContext.Orders.Add(order);

            var positions = new List<OrderPosition>();
            for(var loop=0; loop<countPositions; loop++)
            {
                var newPosition = new OrderPosition(
                    order,
                    loop + 1,
                    CreateRandomString(random, 13),
                    random.NextDouble() * 10.0);
                positions.Add(newPosition);
            }

            await using var transactionScope = await dbContext.Database.BeginTransactionAsync();
            await dbContext.SaveChangesAsync();
            await dbContext.BulkInsertAsync(positions);
            await transactionScope.CommitAsync();
        }

        private static string CreateRandomString(Random random, int length)
        {
            var strBuilder = new StringBuilder(length);
            for (var loop = 0; loop < length; loop++)
            {
                strBuilder.Append((char)('0' + random.Next(0, 10)));
            }
            return strBuilder.ToString();
        }
    }
}
