using MediatR;

namespace HappyCoding.HexagonalArchitecture.Application.Dtos;

public record CreateWorkshopRequest : IRequest<WorkshopDto>
{
    public WorkshopDto Workshop { get; init; }
}