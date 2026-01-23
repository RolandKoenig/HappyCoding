using HappyCoding.HexagonalArchitecture.Domain.Model.Projections;
using MediatR;

namespace HappyCoding.HexagonalArchitecture.Application;

public record SearchWorkshopsQuery() : IRequest<IEnumerable<WorkshopShortInfo>>
{
    public string? QueryString { get; init; }
}