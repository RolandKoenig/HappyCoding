using MediatR;

namespace HappyCoding.HexagonalArchitecture.Application;

public record DeleteWorkshopRequest(Guid WorkshopID) 
    : IRequest;