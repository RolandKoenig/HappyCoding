using HappyCoding.ConsoleLogWindow.Application.Model;
using HappyCoding.ConsoleLogWindow.Application.Ports;
using HappyCoding.ConsoleLogWindow.Application.Services.UseCaseExecution;

namespace HappyCoding.ConsoleLogWindow.Application.UseCases;

public class SaveDocumentToFileUseCase : IUseCase<DocumentModel, string>
{
    private readonly IDocumentFileHandler _documentFileHandler;

    public SaveDocumentToFileUseCase(
        IDocumentFileHandler documentFileHandler)
    {
        _documentFileHandler = documentFileHandler;
    }

    /// <inheritdoc />
    public async Task ExecuteAsync(DocumentModel document, string targetFile)
    {
        await _documentFileHandler.SaveDocumentToFileAsync(document, targetFile);
    }
}
