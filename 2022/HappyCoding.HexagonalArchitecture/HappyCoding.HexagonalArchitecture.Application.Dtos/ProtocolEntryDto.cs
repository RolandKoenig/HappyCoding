namespace HappyCoding.HexagonalArchitecture.Application.Dtos;

public record ProtocolEntryDto
{
    public string Text { get; private set; }
    
    public ProtocolEntryTypeDto EntryType { get; private set; }
    
    public int Priority { get; private set; }
    
    public string Responsible { get; private set; }
    
    public DateTimeOffset ChangeDate { get; private set; }
}