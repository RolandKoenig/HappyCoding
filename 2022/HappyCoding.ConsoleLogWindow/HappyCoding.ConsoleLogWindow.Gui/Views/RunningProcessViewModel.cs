using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using HappyCoding.ConsoleLogWindow.Application.Messages;
using HappyCoding.ConsoleLogWindow.Application.Model;
using HappyCoding.ConsoleLogWindow.Application.Ports;
using HappyCoding.ConsoleLogWindow.Application.UseCases;
using HappyCoding.ConsoleLogWindow.Gui.Messages;
using HappyCoding.ConsoleLogWindow.Gui.Util;
using HappyCoding.ConsoleLogWindow.Messenger;

namespace HappyCoding.ConsoleLogWindow.Gui.Views;

public class RunningProcessViewModel : ViewModelBase
{
    private static readonly ObservableCollection<ProcessOutputLine> EMPTY_PROCESS_OUTPUT = new();

    private readonly IProcessRunner _processRunner;
    private readonly IFirLibMessageSubscriber _messageSubscriber;
    private readonly StartProcessUseCase _startProcessUseCase;

    private IEnumerable<MessageSubscription>? _msgSubscriptions;

    public ObservableCollection<ProcessOutputLine> ProcessOutput { get; private set; }

    public ProcessInfo? SelectedProcess { get; private set; }

    public bool IsRunning { get; private set; }

    public DelegateCommand Command_StartProcess { get; private set; }

    public RunningProcessViewModel(
        IProcessRunner processRunner,
        IFirLibMessageSubscriber messageSubscriber,
        StartProcessUseCase startProcessUseCase)
    {
        _processRunner = processRunner;
        _messageSubscriber = messageSubscriber;
        _startProcessUseCase = startProcessUseCase;

        this.ProcessOutput = EMPTY_PROCESS_OUTPUT;

        this.Command_StartProcess = new DelegateCommand(
            this.StartProcess,
            () => !this.IsRunning);
    }

    /// <inheritdoc />
    public override void ViewLoaded(IView view)
    {
        base.ViewLoaded(view);

        if (_msgSubscriptions == null)
        {
            _msgSubscriptions = _messageSubscriber.SubscribeAll(this);
        }
    }

    /// <inheritdoc />
    public override void ViewUnloaded()
    {
        base.ViewUnloaded();

        if (_msgSubscriptions != null)
        {
            foreach (var actSubscription in _msgSubscriptions)
            {
                actSubscription.Dispose();
            }
            _msgSubscriptions = null;
        }
    }

    private async void StartProcess()
    {
        if(this.SelectedProcess == null)
        {
            return;
        }

        await _startProcessUseCase.ExecuteAsync(this.SelectedProcess);
    }

    private async void OnMessageReceived(ProcessInfoSelectionChangedMessage message)
    {
        this.SelectedProcess = message.SelectedProcessNew;

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
    }

    private async void OnMessageReceived(ProcessStartedMessage message)
    {
        if (message.ProcessInfo.ID != this.SelectedProcess?.ID) { return; }

        var runningProcess = await _processRunner.TryGetRunningProcessAsync(this.SelectedProcess);
        this.IsRunning = runningProcess != null;
        this.ProcessOutput = runningProcess?.Output ?? EMPTY_PROCESS_OUTPUT;

        this.RaisePropertyChanged(nameof(this.IsRunning));
        this.RaisePropertyChanged(nameof(this.ProcessOutput));
    }
}
