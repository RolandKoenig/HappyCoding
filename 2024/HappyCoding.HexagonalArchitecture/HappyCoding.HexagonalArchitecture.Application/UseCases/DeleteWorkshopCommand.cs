using MediatR;

namespace HappyCoding.HexagonalArchitecture.Application.UseCases;

public record DeleteWorkshopCommand(Guid WorkshopID) 
    : IRequest;