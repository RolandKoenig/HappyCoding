using System.Collections.ObjectModel;
using System.Threading;
using HappyCoding.GRpcCommunication.ServerApp.Messages;
using HappyCoding.GRpcCommunication.ServerApp.ServerHost;
using RolandK.Patterns;
using RolandK.Patterns.Mvvm;

namespace HappyCoding.GRpcCommunication.ServerApp.Views;

public class ServerViewModel : PropertyChangedBase
{
    private const int MAX_SERVER_LOG_COUNT = 1024;

    public DelegateCommand Command_StartServer { get; }

    public DelegateCommand Command_StopServer { get; }

    public DelegateCommand Command_ClearLog { get; }

    private AspNetCoreServerHost ServerHost { get; }

    public bool IsBusy { get; private set; }

    public bool IsNotBusy => !this.IsBusy;

    public ObservableCollection<ServerLogEvent> ServerLog { get; }

    public ServerViewModel()
    {
        this.ServerLog = new ObservableCollection<ServerLogEvent>();
        this.ServerHost = new AspNetCoreServerHost();

        this.Command_StartServer = new DelegateCommand(
            this.StartServerAsync,
            () => !this.ServerHost.IsStarted);
        this.Command_StopServer = new DelegateCommand(
            this.StopServerAsync,
            () => this.ServerHost.IsStarted);
        this.Command_ClearLog = new DelegateCommand(() => this.ServerLog.Clear());
    }

    public async void StartServerAsync()
    {
        this.IsBusy = true;
        try
        {
            this.UpdateDialogState();

            await this.ServerHost.StartAsync(CancellationToken.None);
        }
        finally
        {
            this.IsBusy = false;
        }

        this.UpdateDialogState();
    }

    public async void StopServerAsync()
    {
        this.IsBusy = true;
        try
        {
            this.UpdateDialogState();

            await this.ServerHost.StopAsync(CancellationToken.None);
        }
        finally
        {
            this.IsBusy = false;
        }

        this.UpdateDialogState();
    }

    public void OnMessageReceived(ServerLogReceivedMessage message)
    {
        this.ServerLog.Insert(
            0, new ServerLogEvent(message.Timestamp, message.LogLevel, message.Message));

        while (this.ServerLog.Count > MAX_SERVER_LOG_COUNT)
        {
            this.ServerLog.RemoveAt(MAX_SERVER_LOG_COUNT);
        }
    }

    private void UpdateDialogState()
    {
        this.RaisePropertyChanged(nameof(this.IsBusy));
        this.RaisePropertyChanged(nameof(this.IsNotBusy));
        this.Command_StartServer.RaiseCanExecuteChanged();
        this.Command_StopServer.RaiseCanExecuteChanged();
    }
}
