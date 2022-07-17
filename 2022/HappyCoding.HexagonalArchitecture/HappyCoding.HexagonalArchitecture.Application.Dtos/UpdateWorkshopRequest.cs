using MediatR;

namespace HappyCoding.HexagonalArchitecture.Application.Dtos;

public class UpdateWorkshopRequest : IRequest<WorkshopDto>
{
    public WorkshopDto Workshop { get; init; }
}