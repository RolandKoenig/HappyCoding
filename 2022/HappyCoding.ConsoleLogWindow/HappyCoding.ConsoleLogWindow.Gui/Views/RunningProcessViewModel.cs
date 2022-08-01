using System.Collections.ObjectModel;
using HappyCoding.ConsoleLogWindow.Application.Events;
using HappyCoding.ConsoleLogWindow.Application.Model;
using HappyCoding.ConsoleLogWindow.Application.Ports;
using HappyCoding.ConsoleLogWindow.Application.Services.UseCaseExecution;
using HappyCoding.ConsoleLogWindow.Application.UseCases;
using HappyCoding.ConsoleLogWindow.Gui.Events;
using HappyCoding.ConsoleLogWindow.Gui.Util;
using HappyCoding.ConsoleLogWindow.Messenger;

namespace HappyCoding.ConsoleLogWindow.Gui.Views;

public class RunningProcessViewModel : ViewModelBase
{
    private static readonly ObservableCollection<ProcessOutputLine> EMPTY_PROCESS_OUTPUT = new();

    private readonly IProcessRunner _processRunner;
    private readonly IFirLibMessageSubscriber _messageSubscriber;
    private readonly IUseCaseExecutor _useCaseExecutor;

    public ObservableCollection<ProcessOutputLine> ProcessOutput { get; private set; }

    public ProcessInfo? SelectedProcess { get; private set; }

    public bool IsRunning { get; private set; }

    public DelegateCommand Command_StartProcess { get; private set; }
    
    public DelegateCommand Command_StopProcess { get; private set; }

    public RunningProcessViewModel(
        IProcessRunner processRunner,
        IFirLibMessageSubscriber messageSubscriber,
        IUseCaseExecutor useCaseExecutor)
    {
        _processRunner = processRunner;
        _messageSubscriber = messageSubscriber;
        _useCaseExecutor = useCaseExecutor;

        this.ProcessOutput = EMPTY_PROCESS_OUTPUT;

        // Setup commands
        this.Command_StartProcess = new DelegateCommand(
            () => _useCaseExecutor.ExecuteUseCaseAsync<StartProcessUseCase, ProcessInfo>(this.SelectedProcess!).FireAndForget(),
            () => !this.IsRunning);
        this.Command_StopProcess = new DelegateCommand(
            () => _useCaseExecutor.ExecuteUseCaseAsync<StopProcessUseCase, ProcessInfo>(this.SelectedProcess!).FireAndForget(),
            () => this.IsRunning);
    }

    private async void OnMessageReceived(ProcessInfoSelectionChangedEvent eventData)
    {
        this.SelectedProcess = eventData.SelectedProcessNew;

        if (this.SelectedProcess == null)
        {
            this.IsRunning = false;
            this.ProcessOutput = EMPTY_PROCESS_OUTPUT;
        }
        else
        {
            var runningProcess = await _processRunner.TryGetRunningProcessAsync(this.SelectedProcess);
            this.IsRunning = runningProcess != null;
            this.ProcessOutput = runningProcess?.Output ?? EMPTY_PROCESS_OUTPUT;
        }

        this.RaisePropertyChanged(nameof(this.SelectedProcess));
        this.RaisePropertyChanged(nameof(this.IsRunning));
        this.RaisePropertyChanged(nameof(this.ProcessOutput));
        this.Command_StopProcess.RaiseCanExecuteChanged();
        this.Command_StartProcess.RaiseCanExecuteChanged();
    }

    private async void OnMessageReceived(ProcessStartedEvent eventData)
    {
        if (this.SelectedProcess == null)
        {
            return;
        }
        
        var runningProcess = await _processRunner.TryGetRunningProcessAsync(this.SelectedProcess);
        this.IsRunning = runningProcess != null;
        this.ProcessOutput = runningProcess?.Output ?? EMPTY_PROCESS_OUTPUT;

        this.RaisePropertyChanged(nameof(this.IsRunning));
        this.RaisePropertyChanged(nameof(this.ProcessOutput));
        this.Command_StopProcess.RaiseCanExecuteChanged();
        this.Command_StartProcess.RaiseCanExecuteChanged();
    }

    private async void OnMessageReceived(ProcessStoppedEvent eventData)
    {
        if (this.SelectedProcess == null)
        {
            return;
        }
        
        var runningProcess = await _processRunner.TryGetRunningProcessAsync(this.SelectedProcess);
        this.IsRunning = runningProcess != null;
        this.ProcessOutput = runningProcess?.Output ?? EMPTY_PROCESS_OUTPUT;

        this.RaisePropertyChanged(nameof(this.IsRunning));
        this.RaisePropertyChanged(nameof(this.ProcessOutput));
        this.Command_StopProcess.RaiseCanExecuteChanged();
        this.Command_StartProcess.RaiseCanExecuteChanged();
    }
}
