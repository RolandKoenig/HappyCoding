using System;
using System.Collections.Generic;
using System.Threading;
using Avalonia.Threading;
using HappyCoding.GRpcCommunication.ServerApp.Messages;
using HappyCoding.GRpcCommunication.ServerApp.ServerHost;
using RolandK.Patterns.Mvvm;
using RolandK.Utils.Collections;

namespace HappyCoding.GRpcCommunication.ServerApp.Views;

public class ServerViewModel : ViewModelBase
{
    private const int MAX_SERVER_LOG_COUNT = 1024;

    private RingBuffer<ServerLogEvent> _receivedLogEvents;
    private DispatcherTimer? _logRefreshTimer;

    public DelegateCommand Command_StartServer { get; }

    public DelegateCommand Command_StopServer { get; }

    public DelegateCommand Command_ClearLog { get; }

    public DelegateCommand Command_Refresh { get; }

    private AspNetCoreServerHost ServerHost { get; }

    public bool IsBusy { get; private set; }

    public bool IsNotBusy => !this.IsBusy;

    public List<ServerLogEvent> ServerLog { get; private set; }

    public bool AutoRefreshEnabled { get; set; } = true;

    public ServerViewModel()
    {
        _receivedLogEvents = new RingBuffer<ServerLogEvent>(MAX_SERVER_LOG_COUNT);

        this.ServerLog = new List<ServerLogEvent>();
        this.ServerHost = new AspNetCoreServerHost();

        this.Command_StartServer = new DelegateCommand(
            this.StartServerAsync,
            () => !this.ServerHost.IsStarted);
        this.Command_StopServer = new DelegateCommand(
            this.StopServerAsync,
            () => this.ServerHost.IsStarted);
        this.Command_ClearLog = new DelegateCommand(this.ClearLog);
        this.Command_Refresh = new DelegateCommand(this.RefreshLog);
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

    /// <inheritdoc />
    protected override void OnMvvmViewAttached()
    {
        base.OnMvvmViewAttached();

        if (_logRefreshTimer != null)
        {
            _logRefreshTimer.Stop();
            _logRefreshTimer = null;
        }

        _logRefreshTimer = new DispatcherTimer(
            TimeSpan.FromMilliseconds(500),
            DispatcherPriority.Normal,
            (_, _) =>
            {
                if (this.AutoRefreshEnabled)
                {
                    this.RefreshLog();
                }
            });
        _logRefreshTimer.Start();
    }

    /// <inheritdoc />
    protected override void OnMvvmViewDetaching()
    {
        base.OnMvvmViewDetaching();

        if (_logRefreshTimer != null)
        {
            _logRefreshTimer.Stop();
            _logRefreshTimer = null;
        }
    }

    /// <summary>
    /// Copies all currently received log events to ServerLog collection for UI.
    /// </summary>
    private void RefreshLog()
    {
        var newServerLog = new List<ServerLogEvent>();
        for (var loop = _receivedLogEvents.Count - 1; loop >= 0; loop--)
        {
            newServerLog.Add(_receivedLogEvents[loop]);
        }

        this.ServerLog = newServerLog;
        this.RaisePropertyChanged(nameof(this.ServerLog));
    }

    private void ClearLog()
    {
        _receivedLogEvents.Clear();
        this.RefreshLog();
    }

    private void OnMessageReceived(ServerLogReceivedMessage message)
    {
        _receivedLogEvents.Add(new ServerLogEvent(message.Timestamp, message.LogLevel, message.Message));
    }

    private void UpdateDialogState()
    {
        this.RaisePropertyChanged(nameof(this.IsBusy));
        this.RaisePropertyChanged(nameof(this.IsNotBusy));
        this.Command_StartServer.RaiseCanExecuteChanged();
        this.Command_StopServer.RaiseCanExecuteChanged();
    }
}
