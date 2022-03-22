using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace HappyCoding.SqlServerPartitionedSensorDataTable;

public class SensorData
{
    public Guid ID { get; init; }

    public DateTimeOffset Timestamp { get; init; }

    public string SensorName { get; init; } = string.Empty;

    public float SensorValue { get; init; }
}