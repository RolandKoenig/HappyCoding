using System.Diagnostics;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace HappyCoding.InMemoryCacheUsage;

internal class Program
{
    static async Task Main(string[] args)
    {
        // Create cache
        var memoryCache = new MemoryCache(new CacheOptions());
        
        // 
        var randomizerTime = new Random(1);
        
        while (true)
        {
            await Task.Delay(randomizerTime.Next(500, 700));

            var longestCall = TimeSpan.Zero;
            Parallel.For(1, 30000, actCounter =>
            {
                var randomizerName = new Random(actCounter);
                var randomName = randomizerName.Next(1, 5000000).ToString();

                var stopWatch = Stopwatch.StartNew();
                memoryCache.GetOrCreate(randomName, cacheEntry =>
                {
                    cacheEntry.SlidingExpiration = TimeSpan.FromMinutes(2.0);
                    cacheEntry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10.0);
                    return new object();
                });
                stopWatch.Stop();

                if (stopWatch.Elapsed > longestCall)
                {
                    longestCall = stopWatch.Elapsed;
                }
            });

            Console.WriteLine($"longest call: { longestCall.TotalMilliseconds:N3} ms");
        }
    }

    public class CacheOptions : IOptions<MemoryCacheOptions>
    {
        public MemoryCacheOptions Value { get; } = new MemoryCacheOptions();
    }
}
