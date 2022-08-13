using HappyCoding.HexagonalArchitecture.Dtos;
using MediatR;

namespace HappyCoding.HexagonalArchitecture.Application;

public class UpdateWorkshopRequest : IRequest<WorkshopDto>
{
    public WorkshopDto Workshop { get; init; }
}