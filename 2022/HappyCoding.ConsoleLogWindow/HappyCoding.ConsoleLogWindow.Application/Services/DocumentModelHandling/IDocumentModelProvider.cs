using HappyCoding.ConsoleLogWindow.Application.Model;

namespace HappyCoding.ConsoleLogWindow.Application.Services.DocumentModelHandling;

public interface IDocumentModelProvider
{
    DocumentModel GetCurrentDocumentModel();

    DocumentModel CloseAndCreateNew();

    void ChangeCurrentDocumentModel(DocumentModel newDocumentModel);

    void NotifyCurrentDocumentContentChanged();
}
