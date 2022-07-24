using System.Collections.Immutable;
using System.Collections.ObjectModel;
using HappyCoding.ConsoleLogWindow.Domain.Model;
using HappyCoding.ConsoleLogWindow.Domain.Ports;
using HappyCoding.ConsoleLogWindow.Gui.Util;

namespace HappyCoding.ConsoleLogWindow.Gui.Views;

public class ProcessGroupsViewModel : ViewModelBase
{
    private readonly IProcessGroupRepository _processGroupRepo;
    private readonly IProcessRunner _processRunner;

    public ObservableCollection<ProcessGroup> ProcessGroups { get; }

    public ProcessGroupsViewModel(
        IProcessGroupRepository processGroupRepo,
        IProcessRunner processRunner)
    {
        _processGroupRepo = processGroupRepo;
        _processRunner = processRunner;

        this.ProcessGroups = new ObservableCollection<ProcessGroup>();
    }

    /// <inheritdoc />
    public override void ViewLoaded(IView view)
    {
        base.ViewLoaded(view);

        this.ProcessGroups.Add(
            new ProcessGroup()
            {
                Title = "Dummy Group 1",
                Processes = ImmutableArray.Create(new[]
                {
                    new ProcessInfo()
                    {
                        Title = "TestService 1",
                        CommandLine = "HappyCoding.ConsoleLogWindow.TestService.exe"
                    },
                    new ProcessInfo()
                    {
                        Title = "TestService 2",
                        CommandLine = "HappyCoding.ConsoleLogWindow.TestService.exe"
                    }
                })
            });
        this.ProcessGroups.Add(
            new ProcessGroup()
            {
                Title = "Dummy Group 2",
                Processes = ImmutableArray.Create(new[]
                {
                    new ProcessInfo()
                    {
                        Title = "TestService 1",
                        CommandLine = "HappyCoding.ConsoleLogWindow.TestService.exe"
                    },
                    new ProcessInfo()
                    {
                        Title = "TestService 2",
                        CommandLine = "HappyCoding.ConsoleLogWindow.TestService.exe"
                    },
                    new ProcessInfo()
                    {
                        Title = "TestService 3",
                        CommandLine = "HappyCoding.ConsoleLogWindow.TestService.exe"
                    }
                })
            });
    }
}
