using MediatR;

namespace HappyCoding.HexagonalArchitecture.Application.Dtos;

public class SearchWorkshopsRequest : IRequest<IEnumerable<WorkshopShortInfoDto>>
{
    public string QueryString { get; init; }
}