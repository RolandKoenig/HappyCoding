using HappyCoding.HexagonalArchitecture.Domain.Model;
using MediatR;

namespace HappyCoding.HexagonalArchitecture.Application.UseCases;

public record UpdateWorkshopCommand(
    Guid ID, 
    string Project,
    string Title,
    DateTimeOffset StartTimestamp,
    IEnumerable<ProtocolEntry> Protocol) 
    : IRequest<Workshop>;