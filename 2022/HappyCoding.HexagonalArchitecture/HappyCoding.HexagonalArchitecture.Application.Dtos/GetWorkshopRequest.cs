using MediatR;

namespace HappyCoding.HexagonalArchitecture.Application.Dtos;

public class GetWorkshopRequest : IRequest<WorkshopDto>
{
    public Guid ID { get; init; }
}