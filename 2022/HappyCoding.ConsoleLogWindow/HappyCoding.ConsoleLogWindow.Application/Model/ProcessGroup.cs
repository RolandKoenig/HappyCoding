using System.Collections.Immutable;
using System.Collections.ObjectModel;

namespace HappyCoding.ConsoleLogWindow.Application.Model;

public class ProcessGroup
{
    public string Title { get; set; } = string.Empty;

    public ObservableCollection<ProcessInfo> Processes { get; set; } = new();
}
