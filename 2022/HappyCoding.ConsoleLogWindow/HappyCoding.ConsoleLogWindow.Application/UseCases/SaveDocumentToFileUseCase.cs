using HappyCoding.ConsoleLogWindow.Application.Model;
using HappyCoding.ConsoleLogWindow.Application.Services.UseCaseExecution;

namespace HappyCoding.ConsoleLogWindow.Application.UseCases;

public class SaveDocumentToFileUseCase : IUseCase<DocumentModel, string>
{
    /// <inheritdoc />
    public Task ExecuteAsync(DocumentModel arg0, string arg1)
    {
        throw new NotImplementedException();
    }
}
