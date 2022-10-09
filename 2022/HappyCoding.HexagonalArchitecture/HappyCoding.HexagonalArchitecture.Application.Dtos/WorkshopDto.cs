namespace HappyCoding.HexagonalArchitecture.Application.Dtos;

public record WorkshopDto : WorkshopWithoutIDDto
{
    public Guid ID { get; init; }
}