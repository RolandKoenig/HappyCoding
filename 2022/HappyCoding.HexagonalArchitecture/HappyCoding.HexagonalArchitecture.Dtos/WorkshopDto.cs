namespace HappyCoding.HexagonalArchitecture.Dtos;

public record WorkshopDto : WorkshopWithoutIDDto
{
    public Guid ID { get; init; }
}