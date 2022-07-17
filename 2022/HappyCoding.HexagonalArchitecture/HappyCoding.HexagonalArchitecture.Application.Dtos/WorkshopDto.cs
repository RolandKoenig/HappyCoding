namespace HappyCoding.HexagonalArchitecture.Application.Dtos;

public record WorkshopDto
{
    public Guid ID { get; init; }
    
    public string Project { get; init; }
    
    public string Title { get; init; }
    
    public DateTimeOffset StartTimestamp { get; init; }
    
    public TimeSpan Duration { get; init; }
    
    public List<string> Participants { get; init; }
    
    public List<ProtocolEntryDto> Protocol { get; init; }
}