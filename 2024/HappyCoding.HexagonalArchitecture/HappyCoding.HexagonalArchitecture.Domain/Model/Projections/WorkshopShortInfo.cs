namespace HappyCoding.HexagonalArchitecture.Domain.Model.Projections;

public record WorkshopShortInfo
{
    public Guid ID { get; init; } 

    public string Project { get; init; } = "";

    public string Title { get; init; } = "";
    
    public DateTimeOffset StartTimestamp { get; init; }
}