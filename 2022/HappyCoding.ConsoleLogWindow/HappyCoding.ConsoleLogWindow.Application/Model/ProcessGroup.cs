using System.Collections.Immutable;

namespace HappyCoding.ConsoleLogWindow.Application.Model;

public class ProcessGroup
{
    public string Title { get; set; } = string.Empty;

    public List<ProcessInfo> Processes { get; set; } = new();
}
