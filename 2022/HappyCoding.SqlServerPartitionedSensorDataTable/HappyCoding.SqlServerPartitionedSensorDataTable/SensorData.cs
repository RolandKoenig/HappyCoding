using System.ComponentModel.DataAnnotations;

namespace HappyCoding.SqlServerPartitionedSensorDataTable;

public class SensorData
{
    [Key]
    public DateTimeOffset Timestamp { get; init; }

    [StringLength(30)]
    public string SensorName { get; init; } = string.Empty;

    public float SensorValue { get; init; }
}