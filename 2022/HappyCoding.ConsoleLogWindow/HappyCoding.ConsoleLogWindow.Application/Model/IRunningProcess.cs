using System.Collections.ObjectModel;

namespace HappyCoding.ConsoleLogWindow.Application.Model;

public interface IRunningProcess : IDisposable
{
    ObservableCollection<ProcessOutputLine> Output { get; }

    bool IsRunning { get; }
}