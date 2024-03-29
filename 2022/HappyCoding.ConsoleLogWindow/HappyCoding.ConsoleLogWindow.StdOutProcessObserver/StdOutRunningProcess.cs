﻿using System.Collections.ObjectModel;
using System.Diagnostics;
using HappyCoding.ConsoleLogWindow.Application.Model;

namespace HappyCoding.ConsoleLogWindow.StdOutProcessRunner;

internal class StdOutRunningProcess : IRunningProcess
{
    private readonly Process _process;
    private readonly ObservableCollection<ProcessOutputLine> _output;

    private bool _disposed;

    /// <inheritdoc />
    public ObservableCollection<ProcessOutputLine> Output => _output;

    /// <inheritdoc />
    public bool IsRunning => !_process.HasExited;

    internal StdOutRunningProcess(Process process)
    {
        _process = process;
        _output = new ObservableCollection<ProcessOutputLine>();

        _process.BeginErrorReadLine();
        _process.BeginOutputReadLine();
        _process.OutputDataReceived += this.OnOutputDataReceived;
        _process.ErrorDataReceived += this.OnErrorDataReceived;
    }

    internal void Kill()
    {
        _process.Kill();
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            _process.OutputDataReceived -= this.OnOutputDataReceived;
            _process.ErrorDataReceived -= this.OnErrorDataReceived;

            _process.Dispose();

            _disposed = true;
        }
    }

    private void AddLine(string text)
    {
        var newLine = new ProcessOutputLine()
        {
            Timestamp = DateTimeOffset.UtcNow,
            Text = text
        };

        _output.Add(newLine);
    }

    private void OnOutputDataReceived(object sender, DataReceivedEventArgs args)
    {
        if (args.Data == null) { return; }

        AddLine(args.Data);
    }

    private void OnErrorDataReceived(object sender, DataReceivedEventArgs args)
    {
        if (args.Data == null) { return; }

        AddLine(args.Data);
    }
}
