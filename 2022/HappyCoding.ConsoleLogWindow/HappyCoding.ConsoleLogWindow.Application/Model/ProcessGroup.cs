using System.Collections.Immutable;

namespace HappyCoding.ConsoleLogWindow.Application.Model;

public record ProcessGroup
{
    public Guid ID { get; init; } = Guid.NewGuid();

    public string Title { get; init; } = string.Empty;

    public ImmutableArray<ProcessInfo> Processes { get; init; } = ImmutableArray.Create(Array.Empty<ProcessInfo>());
}
