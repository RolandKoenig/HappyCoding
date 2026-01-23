namespace HappyCoding.HexagonalArchitecture.WebUI.Dtos;

public record WorkshopDto : WorkshopWithoutIDDto
{
    public Guid ID { get; init; }
}