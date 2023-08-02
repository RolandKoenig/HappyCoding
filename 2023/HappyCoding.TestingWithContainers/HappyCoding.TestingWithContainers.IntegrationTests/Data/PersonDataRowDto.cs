namespace HappyCoding.TestingWithContainers.IntegrationTests.Data;

public record PersonDataRow
{
    public int Id { get; init; }

    public string Name { get; init; } = string.Empty;
    
    public string City { get; init; } = string.Empty;
}