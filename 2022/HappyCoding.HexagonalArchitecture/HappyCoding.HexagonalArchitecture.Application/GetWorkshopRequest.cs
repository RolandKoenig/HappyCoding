using HappyCoding.HexagonalArchitecture.Dtos;
using MediatR;

namespace HappyCoding.HexagonalArchitecture.Application;

public class GetWorkshopRequest : IRequest<WorkshopDto>
{
    public Guid ID { get; init; }
}