using HappyCoding.HexagonalArchitecture.Application.Model.Projections;
using MediatR;

namespace HappyCoding.HexagonalArchitecture.Application.UseCases;

public record SearchWorkshopsQuery() : IRequest<IEnumerable<WorkshopShortInfo>>
{
    public string? QueryString { get; init; }
}