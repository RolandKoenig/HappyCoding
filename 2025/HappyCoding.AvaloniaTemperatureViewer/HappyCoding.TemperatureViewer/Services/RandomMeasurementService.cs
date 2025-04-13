using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using HappyCoding.TemperatureViewer.Model;

namespace HappyCoding.TemperatureViewer.Services;

public class RandomMeasurementService : IMeasurementService
{
    public async IAsyncEnumerable<TemperatureMeasurement> StartMeasurement(
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        var random = new Random();
        while (!cancellationToken.IsCancellationRequested)
        {
            yield return new TemperatureMeasurement(
                DateTimeOffset.UtcNow,
                16 + random.NextDouble() * 16);
            
            try
            {
                await Task.Delay(1000, cancellationToken);
            }
            catch (OperationCanceledException)
            {
                break;
            }
        }
    }
}