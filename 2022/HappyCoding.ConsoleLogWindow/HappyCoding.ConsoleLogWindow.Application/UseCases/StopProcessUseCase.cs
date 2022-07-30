using HappyCoding.ConsoleLogWindow.Application.Exceptions;
using HappyCoding.ConsoleLogWindow.Application.Events;
using HappyCoding.ConsoleLogWindow.Application.Model;
using HappyCoding.ConsoleLogWindow.Application.Ports;
using HappyCoding.ConsoleLogWindow.Application.Services.UseCaseExecution;
using HappyCoding.ConsoleLogWindow.Messenger;

namespace HappyCoding.ConsoleLogWindow.Application.UseCases;

public class StopProcessUseCase : IUseCase<ProcessInfo>
{
    private readonly IProcessRunner _processRunner;
    private readonly IFirLibMessagePublisher _messagePublisher;

    public StopProcessUseCase(
        IProcessRunner processRunner,
        IFirLibMessagePublisher messagePublisher)
    {
        _processRunner = processRunner;
        _messagePublisher = messagePublisher;
    }

    public async Task ExecuteAsync(ProcessInfo processToStart)
    {
        var processRunning = await _processRunner.IsProcessRunningAsync(processToStart);
        if (!processRunning)
        {
            throw new ConsoleLogWindowApplicationException($"Process '{processToStart.Title}' is not running!");
        }

        await _processRunner.StopProcessAsync(processToStart);

        _messagePublisher.Publish(new ProcessStoppedEvent(processToStart));
    }
}
