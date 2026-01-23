using HappyCoding.HexagonalArchitecture.Domain.Model;
using MediatR;

namespace HappyCoding.HexagonalArchitecture.Application;

public record UpdateWorkshopCommand(
    Guid ID, 
    string Project,
    string Title,
    DateTimeOffset StartTimestamp,
    IEnumerable<ProtocolEntry> Protocol) 
    : IRequest<Workshop>;