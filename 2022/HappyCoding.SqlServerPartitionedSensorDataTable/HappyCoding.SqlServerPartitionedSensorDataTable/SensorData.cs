using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HappyCoding.SqlServerPartitionedSensorDataTable;

public class SensorData
{    
    [Key]
    public Guid ID { get; init; }

    public DateTimeOffset Timestamp { get; init; }

    [StringLength(30)]
    public string SensorName { get; init; } = string.Empty;

    public float SensorValue { get; init; }
}