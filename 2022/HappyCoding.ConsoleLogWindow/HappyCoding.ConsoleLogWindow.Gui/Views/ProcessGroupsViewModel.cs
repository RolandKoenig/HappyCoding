using System.Collections.Immutable;
using System.Collections.ObjectModel;
using HappyCoding.ConsoleLogWindow.Application.Model;
using HappyCoding.ConsoleLogWindow.Application.Ports;
using HappyCoding.ConsoleLogWindow.Gui.Messages;
using HappyCoding.ConsoleLogWindow.Gui.Util;
using HappyCoding.ConsoleLogWindow.Messenger;

namespace HappyCoding.ConsoleLogWindow.Gui.Views;

public class ProcessGroupsViewModel : ViewModelBase
{
    private readonly IProcessGroupRepository _processGroupRepo;
    private readonly IProcessRunner _processRunner;
    private readonly IFirLibMessagePublisher _messagePublisher;

    private object? _selectedObject;

    public ObservableCollection<ProcessGroup> ProcessGroups { get; }

    public object? SelectedObject
    {
        get => _selectedObject;
        set
        {
            if (_selectedObject != value)
            {
                var prevSelectedObject = _selectedObject;

                _selectedObject = value;
                this.RaisePropertyChanged(nameof(this.SelectedObject));

                _messagePublisher.BeginPublish(
                    new ProcessInfoSelectionChangedMessage()
                    {
                        SelectedProcessOld = prevSelectedObject as ProcessInfo,
                        SelectedProcessNew = _selectedObject as ProcessInfo
                    });
            }
        }
    }

    public ProcessGroupsViewModel(
        IProcessGroupRepository processGroupRepo,
        IProcessRunner processRunner,
        IFirLibMessagePublisher messagePublisher)
    {
        _processGroupRepo = processGroupRepo;
        _processRunner = processRunner;
        _messagePublisher = messagePublisher;

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
                        FileName = "dotnet",
                        Arguments = "HappyCoding.ConsoleLogWindow.TestService.dll"
                    },
                    new ProcessInfo()
                    {
                        Title = "TestService 2",
                        FileName = "dotnet",
                        Arguments = "HappyCoding.ConsoleLogWindow.TestService.dll"
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
                        FileName = "dotnet",
                        Arguments = "HappyCoding.ConsoleLogWindow.TestService.dll"
                    },
                    new ProcessInfo()
                    {
                        Title = "TestService 2",
                        FileName = "dotnet",
                        Arguments = "HappyCoding.ConsoleLogWindow.TestService.dll"
                    },
                    new ProcessInfo()
                    {
                        Title = "TestService 3",
                        FileName = "dotnet",
                        Arguments = "HappyCoding.ConsoleLogWindow.TestService.dll"
                    }
                })
            });
    }
}
