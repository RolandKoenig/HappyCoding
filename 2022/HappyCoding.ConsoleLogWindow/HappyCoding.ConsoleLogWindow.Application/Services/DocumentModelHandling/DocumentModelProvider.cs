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
