using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace HappyCoding.JsonInSqlServer.Util
{
    internal static class MeasureUtil
    {
        public static async Task<TimeSpan> MeasureTimeAsync(int execCount, bool logPerCycle, Func<Task> taskFactory)
        {
            // Cold start
            await taskFactory();

            // Measuring
            var durations = new List<TimeSpan>(execCount);
            for (var loop = 0; loop < execCount; loop++)
            {            
                var stopWatch = new Stopwatch();
                stopWatch.Start();

                await taskFactory();

                stopWatch.Stop();
                durations.Add(stopWatch.Elapsed);

                if (logPerCycle)
                {
                    Console.WriteLine($" - cylce {loop + 1}: {stopWatch.ElapsedMilliseconds:F2} ms");
                }
            }

            return TimeSpan.FromTicks(
                (long)durations.Average(actTimespan => actTimespan.Ticks));
        }   
    }
}
