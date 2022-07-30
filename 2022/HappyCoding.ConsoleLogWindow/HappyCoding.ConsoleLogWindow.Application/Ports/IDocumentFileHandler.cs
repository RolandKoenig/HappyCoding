using HappyCoding.ConsoleLogWindow.Application.Model;

namespace HappyCoding.ConsoleLogWindow.Application.Ports;

public interface IDocumentFileHandler
{
    Task SaveDocumentToFileAsync(DocumentModel model, string fileName);

    Task<DocumentModel> LoadDocumentFromFileAsync(string fileName);
}
