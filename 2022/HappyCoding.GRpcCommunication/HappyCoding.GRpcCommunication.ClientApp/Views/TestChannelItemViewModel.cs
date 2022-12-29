using System;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Media;
using HappyCoding.GRpcCommunication.ClientApp.TestChannels;
using RolandK.Patterns;
using RolandK.Patterns.Mvvm;

namespace HappyCoding.GRpcCommunication.ClientApp.Views;

public class TestChannelItemViewModel : PropertyChangedBase
{
    private readonly ITestChannel _testChannel;
    private bool _isStarting;
    private ulong _countSuccess;
    private ulong _countSpikes;
    private ulong _countTimeouts;
    private ulong _countErrors;
    private double _callDurationAvgMS;
    private double _callDurationMinMS;
    private double _callDurationMaxMS;
    private double _callsPerSecond;
    private string _lastErrorDetails = string.Empty;
    private ISolidColorBrush _stateBrush = Brushes.Gray;

    public DelegateCommand Command_Start { get; }

    public DelegateCommand Command_Stop { get; }

    public DelegateCommand Command_ResetMetrics { get; }

    public bool IsStarted { get; private set; }

    public ulong CountSuccess
    {
        get => _countSuccess;
        private set => this.SetProperty(ref _countSuccess, value);
    }

    public ulong CountSpikes
    {
        get => _countSpikes;
        private set => this.SetProperty(ref _countSpikes, value);
    }

    public ulong CountTimeouts
    {
        get => _countTimeouts;
        private set => this.SetProperty(ref _countTimeouts, value);
    }

    public ulong CountErrors
    {
        get => _countErrors;
        private set => this.SetProperty(ref _countErrors, value);
    }

    public string LastErrorDetails
    {
        get => _lastErrorDetails;
        private set => this.SetProperty(ref _lastErrorDetails, value);
    }

    public double CallDurationAvgMS
    {
        get => _callDurationAvgMS;
        set => this.SetProperty(ref _callDurationAvgMS, value);
    }

    public double CallDurationMinMS
    {
        get => _callDurationMinMS;
        set => this.SetProperty(ref _callDurationMinMS, value);
    }

    public double CallDurationMaxMS
    {
        get => _callDurationMaxMS;
        set => this.SetProperty(ref _callDurationMaxMS, value);
    }

    public double CallsPerSecond
    {
        get => _callsPerSecond;
        set => this.SetProperty(ref _callsPerSecond, value);
    }

    public bool IsBusy { get; private set; }

    public bool IsNotBusy => !this.IsBusy;

    public string Title => _testChannel.GetType().Name;

    public ISolidColorBrush StatusBrush
    {
        get => _stateBrush;
        private set => this.SetProperty(ref _stateBrush, value);
    }

    public TestChannelItemViewModel(ITestChannel testChannel)
    {
        _testChannel = testChannel;

        this.Command_Start = new DelegateCommand(
            this.StartTestChannel,
            () => !this.IsStarted);
        this.Command_Stop = new DelegateCommand(
            this.StopTestChannel,
            () => this.IsStarted);
        this.Command_ResetMetrics = new DelegateCommand(
            this.ResetMetrics);
    }

    public void ResetMetrics()
    {
        _testChannel.ResetMetrics();

        this.UpdateLocalProperties();
    }

    private async void StartTestChannel()
    {
        this.IsBusy = true;
        try
        {
            _isStarting = true;
            this.UpdateDialogState();

            var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(5.0));
            await _testChannel.StartAsync(cancellationTokenSource.Token);
            this.IsStarted = true;
        }
        catch (Exception)
        {
            this.IsStarted = false;
        }
        finally
        {
            _isStarting = false;
            this.IsBusy = false;
        }

        if (this.IsStarted)
        {
            this.RunStateWatchTask();
        }

        this.UpdateDialogState();
    }

    private async void StopTestChannel()
    {
        this.IsBusy = true;
        try
        {
            this.UpdateDialogState();

            await _testChannel.StopAsync(CancellationToken.None);
            this.IsStarted = false;
        }
        finally
        {
            this.IsBusy = false;
        }

        this.UpdateDialogState();
    }

    private async void RunStateWatchTask()
    {
        while (this.IsStarted)
        {
            await Task.Delay(100);

            this.UpdateLocalProperties();
        }
    }

    private void UpdateLocalProperties()
    {
        this.CountSuccess = _testChannel.CountSuccess;
        this.CountSpikes = _testChannel.CountSpikes;
        this.CountTimeouts = _testChannel.CountTimeouts;
        this.CountErrors = _testChannel.CountErrors;
        this.LastErrorDetails = _testChannel.LastErrorDetails;

        this.CallDurationAvgMS = _testChannel.CallDurationAvgMS;
        this.CallDurationMinMS = _testChannel.CallDurationMinMS;
        this.CallDurationMaxMS = _testChannel.CallDurationMaxMS;

        this.CallsPerSecond = _testChannel.CallsPerSecond;

        this.UpdateStateBrush();

        this.RaisePropertyChanged(nameof(this.StatusBrush));
    }

    private void UpdateDialogState()
    {
        this.Command_Start.RaiseCanExecuteChanged();
        this.Command_Stop.RaiseCanExecuteChanged();

        this.UpdateStateBrush();

        this.RaisePropertyChanged(nameof(this.IsBusy));
        this.RaisePropertyChanged(nameof(this.IsNotBusy));

        this.RaisePropertyChanged(nameof(this.IsStarted));
        this.RaisePropertyChanged(nameof(this.StatusBrush));
    }

    private void UpdateStateBrush()
    {
        if (_isStarting) { this.StatusBrush = Brushes.Yellow; }
        else if (!this.IsStarted) { this.StatusBrush = Brushes.Gray; }
        else if (!_testChannel.IsConnected) { this.StatusBrush = Brushes.Yellow; }
        else { this.StatusBrush = Brushes.Green; }
    }
}
