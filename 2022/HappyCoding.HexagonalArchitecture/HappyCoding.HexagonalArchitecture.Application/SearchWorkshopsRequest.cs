using HappyCoding.HexagonalArchitecture.Application.Dtos;
using MediatR;

namespace HappyCoding.HexagonalArchitecture.Application;

public record SearchWorkshopsRequest() : IRequest<IEnumerable<WorkshopShortInfoDto>>
{
    public string? QueryString { get; init; }
}