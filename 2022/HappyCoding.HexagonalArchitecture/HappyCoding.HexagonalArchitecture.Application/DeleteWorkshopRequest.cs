using MediatR;

namespace HappyCoding.HexagonalArchitecture.Application;

public record DeleteWorkshopRequest : IRequest
{
    public Guid WorkshopID { get; init; }
}