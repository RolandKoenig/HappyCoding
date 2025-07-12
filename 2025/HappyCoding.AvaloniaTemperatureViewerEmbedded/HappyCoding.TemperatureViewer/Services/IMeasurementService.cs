using System.Collections.Generic;
using System.Threading;
using HappyCoding.TemperatureViewer.Model;

namespace HappyCoding.TemperatureViewer.Services;

public interface IMeasurementService
{
    IAsyncEnumerable<TemperatureMeasurement> StartMeasurement(CancellationToken cancellationToken);
}