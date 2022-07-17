namespace HappyCoding.HexagonalArchitecture.Application.Dtos;

public record ProtocolEntryDto
{
    public string Text { get; init; }
    
    public ProtocolEntryTypeDto EntryType { get; init; }
    
    public int Priority { get; init; }
    
    public string Responsible { get; init; }
    
    public DateTimeOffset ChangeDate { get; init; }
}