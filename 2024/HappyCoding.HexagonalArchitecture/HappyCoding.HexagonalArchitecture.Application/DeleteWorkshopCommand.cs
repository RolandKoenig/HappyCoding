using MediatR;

namespace HappyCoding.HexagonalArchitecture.Application;

public record DeleteWorkshopCommand(Guid WorkshopID) 
    : IRequest;