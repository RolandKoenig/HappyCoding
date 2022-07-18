namespace HappyCoding.HexagonalArchitecture.Application.Dtos;

public record WorkshopWithoutIDDto
{
    public string Project { get; set; }

    public string Title { get; set; }
    
    public DateTimeOffset StartTimestamp { get; set; }

    public List<ProtocolEntryDto> Protocol { get; set; }
}
