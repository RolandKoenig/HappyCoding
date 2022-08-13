using HappyCoding.HexagonalArchitecture.Dtos;
using MediatR;

namespace HappyCoding.HexagonalArchitecture.Application;

public record CreateWorkshopRequest : IRequest<WorkshopDto>
{
    public WorkshopWithoutIDDto Workshop { get; init; }
}