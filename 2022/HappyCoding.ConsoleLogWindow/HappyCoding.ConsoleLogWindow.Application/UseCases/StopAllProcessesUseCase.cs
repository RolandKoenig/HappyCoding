using HappyCoding.ConsoleLogWindow.Application.Ports;
using HappyCoding.ConsoleLogWindow.Application.Services.UseCaseExecution;
using HappyCoding.ConsoleLogWindow.Messenger;

namespace HappyCoding.ConsoleLogWindow.Application.UseCases;

public class StopAllProcessesUseCase : IUseCaseNoArg
{
    private readonly IProcessRunner _processRunner;
    private readonly IFirLibMessagePublisher _messagePublisher;

    public StopAllProcessesUseCase(
        IProcessRunner processRunner,
        IFirLibMessagePublisher messagePublisher)
    {
        _processRunner = processRunner;
        _messagePublisher = messagePublisher;
    }

    /// <inheritdoc />
    public Task ExecuteAsync()
    {
        throw new NotImplementedException();
    }
}
