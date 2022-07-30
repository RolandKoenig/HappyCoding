using System.Collections.ObjectModel;
using HappyCoding.ConsoleLogWindow.Application.Events;
using HappyCoding.ConsoleLogWindow.Application.Model;
using HappyCoding.ConsoleLogWindow.Messenger;

namespace HappyCoding.ConsoleLogWindow.Application.Services.DocumentModelHandling;

public class DocumentModelProvider : IDocumentModelProvider
{
    private readonly IFirLibMessagePublisher _messagePublisher;

    public DocumentModel CurrentDocumentModel { get; private set; }

    public DocumentModelProvider(IFirLibMessagePublisher messagePublisher)
    {
        _messagePublisher = messagePublisher;
        
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

    /// <inheritdoc />
    public DocumentModel GetCurrentDocumentModel()
    {
        return this.CurrentDocumentModel;
    }

    /// <inheritdoc />
    public DocumentModel CloseAndCreateNew()
    {
        var newDocument = new DocumentModel();

        this.CurrentDocumentModel = newDocument;

        _messagePublisher.BeginPublish(new CurrentDocumentChangedEvent(newDocument));

        return this.CurrentDocumentModel;
    }

    /// <inheritdoc />
    public void ChangeCurrentDocumentModel(DocumentModel newDocumentModel)
    {
        this.CurrentDocumentModel = newDocumentModel;
        _messagePublisher.BeginPublish(new CurrentDocumentChangedEvent(newDocumentModel));
    }

    /// <inheritdoc />
    public void NotifyCurrentDocumentContentChanged()
    {
        _messagePublisher.BeginPublish(new CurrentDocumentContentChangedEvent());
    }
}
