namespace HappyCoding.HexagonalArchitecture.Application.UseCases;

public record SearchWorkshopsQuery()
{
    public string? QueryString { get; init; }
}