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

    public DelegateCommand Command_Start { get; }

    public DelegateCommand Command_Stop { get; }

    public bool IsStarted { get; private set; }

    public ulong CountSuccess => _testChannel.CountSuccess;

    public ulong CountErrors => _testChannel.CountErrors;

    public string LastErrorDetails => _testChannel.LastErrorDetails;


    public bool IsBusy { get; private set; }

    public bool IsNotBusy => !this.IsBusy;

    public string Title => _testChannel.GetType().Name;

    public ISolidColorBrush StatusBrush
    {
        get
        {
            if (!this.IsStarted) { return Brushes.Gray; }
            if (!_testChannel.IsConnected) { return Brushes.Yellow; }
            return Brushes.Green;
        }
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
    }

    private async void StartTestChannel()
    {
        this.IsBusy = true;
        try
        {
            this.UpdateDialogState();

            await _testChannel.StartAsync(CancellationToken.None);
            this.IsStarted = true;

            this.RunStateWatchTask();
        }
        finally
        {
            this.IsBusy = false;
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

            this.RaisePropertyChanged(nameof(this.StatusBrush));
            this.RaisePropertyChanged(nameof(this.CountErrors));
            this.RaisePropertyChanged(nameof(this.CountSuccess));
            this.RaisePropertyChanged(nameof(this.LastErrorDetails));
        }
    }

    private void UpdateDialogState()
    {
        this.Command_Start.RaiseCanExecuteChanged();
        this.Command_Stop.RaiseCanExecuteChanged();

        this.RaisePropertyChanged(nameof(this.IsBusy));
        this.RaisePropertyChanged(nameof(this.IsNotBusy));

        this.RaisePropertyChanged(nameof(this.IsStarted));
        this.RaisePropertyChanged(nameof(this.StatusBrush));
    }
}
