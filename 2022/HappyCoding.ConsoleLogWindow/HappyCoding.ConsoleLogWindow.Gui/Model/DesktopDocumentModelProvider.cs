using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using HappyCoding.ConsoleLogWindow.Application.Model;
using HappyCoding.ConsoleLogWindow.Application.Ports;
using HappyCoding.ConsoleLogWindow.Messenger;

namespace HappyCoding.ConsoleLogWindow.Gui.Model;

public class DesktopDocumentModelProvider : IDocumentModelProvider
{
    private readonly IFirLibMessagePublisher _messagePublisher;

    public string? FileName { get; private set; }

    public DocumentModel CurrentDocumentModel { get; private set; }

    public DesktopDocumentModelProvider(
        IFirLibMessagePublisher messagePublisher)
    {
        _messagePublisher = messagePublisher;

        this.FileName = null;
        this.CurrentDocumentModel = new DocumentModel();

        this.CurrentDocumentModel.ProcessGroups.Add(
            new ProcessGroup()
            {
                Title = "Dummy Group 1",
                Processes = new ObservableCollection<ProcessInfo>()
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
                }
            });
        this.CurrentDocumentModel.ProcessGroups.Add(
            new ProcessGroup()
            {
                Title = "Dummy Group 2",
                Processes = new ObservableCollection<ProcessInfo>()
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
                }
            });
    }

    public Task NewAsync()
    {
        return Task.CompletedTask;
    }

    public Task CloseAsync()
    {
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public DocumentModel GetCurrentDocumentModel()
    {
        return this.CurrentDocumentModel;
    }

    /// <inheritdoc />
    public void NotifyDocumentModelChanged()
    {
        throw new NotImplementedException();
    }
}
