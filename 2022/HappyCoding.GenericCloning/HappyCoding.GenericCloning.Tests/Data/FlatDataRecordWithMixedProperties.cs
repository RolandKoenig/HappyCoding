namespace HappyCoding.GenericCloning.Tests.Data;

public record FlatDataRecordWithMixedProperties(string? Property1, string? Property2)
{
    public int Property3 { get; init; }

    public float Property4 { get; init; }

    public int? Property5 { get; set; }

    public int? Property6 { get; set; }
}