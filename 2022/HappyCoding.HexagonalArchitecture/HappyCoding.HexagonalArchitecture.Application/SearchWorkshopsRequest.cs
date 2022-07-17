using HappyCoding.HexagonalArchitecture.Application.Dtos;
using MediatR;

namespace HappyCoding.HexagonalArchitecture.Application;

public class SearchWorkshopsRequest : IRequest<IEnumerable<WorkshopShortInfoDto>>
{
    public string QueryString { get; init; }
}