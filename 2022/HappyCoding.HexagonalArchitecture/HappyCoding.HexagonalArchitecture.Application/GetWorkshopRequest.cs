using HappyCoding.HexagonalArchitecture.Application.Dtos;
using MediatR;

namespace HappyCoding.HexagonalArchitecture.Application;

public class GetWorkshopRequest : IRequest<WorkshopDto>
{
    public Guid ID { get; init; }
}