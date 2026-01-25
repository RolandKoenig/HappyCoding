using HappyCoding.HexagonalArchitecture.Application.Model;

namespace HappyCoding.HexagonalArchitecture.Application.UseCases;

public record CreateWorkshopCommand(
    string Project,
    string Title,
    DateTimeOffset StartTimestamp,
    IEnumerable<ProtocolEntry> Protocol);