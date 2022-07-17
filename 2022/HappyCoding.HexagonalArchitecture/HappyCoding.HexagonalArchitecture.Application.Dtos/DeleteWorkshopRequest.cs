using MediatR;

namespace HappyCoding.HexagonalArchitecture.Application.Dtos;

public record DeleteWorkshopRequest : IRequest
{
    public Guid WorkshopID { get; init; }
}