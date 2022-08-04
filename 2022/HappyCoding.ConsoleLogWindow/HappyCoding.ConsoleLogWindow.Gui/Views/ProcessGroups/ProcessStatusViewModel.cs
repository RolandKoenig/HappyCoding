using Avalonia.Media;
using HappyCoding.ConsoleLogWindow.Application.Events;
using HappyCoding.ConsoleLogWindow.Application.Model;
using HappyCoding.ConsoleLogWindow.Application.Ports;
using HappyCoding.ConsoleLogWindow.Gui.Util;

namespace HappyCoding.ConsoleLogWindow.Gui.Views.ProcessGroups;

internal class ProcessStatusViewModel : ViewModelBase
{
    private readonly IProcessRunner _processRunner;

    private ProcessInfo? _selectedProcess;
    private bool _isProcessRunning;

    public ProcessInfo? SelectedProcess
    {
        get => _selectedProcess;
        set
        {
            if (_selectedProcess != value)
            {
                _selectedProcess = value;
                this.RaisePropertyChanged(nameof(this.ProcessStatusColor));
                this.RaisePropertyChanged(nameof(this.IsStatusVisible));

                this.UpdateProcessRunningState();
            }
        }
    }

    public Color ProcessStatusColor
    {
        get
        {
            if (_selectedProcess == null) { return Color.FromArgb(0, 0, 0, 0); }
            else if (_isProcessRunning) { return Color.FromArgb(255, 0, 255, 0); }
            else { return Color.FromArgb(255, 128, 128, 128); }
        }
    }

    public bool IsStatusVisible => _selectedProcess != null;

    public ProcessStatusViewModel(IProcessRunner processRunner)
    {
        _processRunner = processRunner;
    }

    private async void UpdateProcessRunningState()
    {
        var runningProcess = _selectedProcess;
        if (runningProcess != null)
        {
            _isProcessRunning = await _processRunner.IsProcessRunningAsync(runningProcess);
        }
        else
        {
            _isProcessRunning = false;
        }
        
        this.RaisePropertyChanged(nameof(this.ProcessStatusColor));
    }

    private void OnMessageReceived(ProcessStartedEvent eventData)
    {
        if (eventData.ProcessInfo != _selectedProcess) { return; }

        _isProcessRunning = true;

        this.RaisePropertyChanged(nameof(this.ProcessStatusColor));
    }

    private void OnMessageReceived(ProcessStoppedEvent eventData)
    {
        if (eventData.ProcessInfo != _selectedProcess) { return; }

        _isProcessRunning = false;

        this.RaisePropertyChanged(nameof(this.ProcessStatusColor));
    }
}
