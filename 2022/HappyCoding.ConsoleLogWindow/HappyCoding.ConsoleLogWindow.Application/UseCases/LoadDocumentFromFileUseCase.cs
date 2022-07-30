using HappyCoding.ConsoleLogWindow.Application.Ports;
using HappyCoding.ConsoleLogWindow.Application.Services.DocumentModelHandling;
using HappyCoding.ConsoleLogWindow.Application.Services.UseCaseExecution;

namespace HappyCoding.ConsoleLogWindow.Application.UseCases;

public class LoadDocumentFromFileUseCase : IUseCase<string>
{
    private readonly IDocumentFileHandler _documentFileHandler;
    private readonly IDocumentModelProvider _documentModelProvider;

    public LoadDocumentFromFileUseCase(
        IDocumentFileHandler documentFileHandler,
        IDocumentModelProvider documentModelProvider)
    {
        _documentFileHandler = documentFileHandler;
        _documentModelProvider = documentModelProvider;
    }

    /// <inheritdoc />
    public async Task ExecuteAsync(string sourceFile)
    {
        var loadedDocument = await _documentFileHandler.LoadDocumentFromFileAsync(sourceFile);

        _documentModelProvider.ChangeCurrentDocumentModel(loadedDocument);
    }
}
