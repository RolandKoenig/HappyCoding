using HappyCoding.ConsoleLogWindow.Application.Model;

namespace HappyCoding.ConsoleLogWindow.Application.Ports;

public interface IDocumentModelProvider
{
    DocumentModel GetCurrentDocumentModel();

    void NotifyDocumentModelChanged();
}
