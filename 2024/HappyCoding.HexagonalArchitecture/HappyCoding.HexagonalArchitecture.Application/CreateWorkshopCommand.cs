using HappyCoding.HexagonalArchitecture.Domain.Model;
using MediatR;

namespace HappyCoding.HexagonalArchitecture.Application;

public record CreateWorkshopCommand(
    string Project,
    string Title,
    DateTimeOffset StartTimestamp,
    IEnumerable<ProtocolEntry> Protocol) 
    : IRequest<Workshop>;