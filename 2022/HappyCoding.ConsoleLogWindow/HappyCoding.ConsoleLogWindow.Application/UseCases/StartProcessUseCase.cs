using HappyCoding.ConsoleLogWindow.Application.Exceptions;
using HappyCoding.ConsoleLogWindow.Application.Messages;
using HappyCoding.ConsoleLogWindow.Application.Model;
using HappyCoding.ConsoleLogWindow.Application.Ports;
using HappyCoding.ConsoleLogWindow.Application.Services.UseCases;
using HappyCoding.ConsoleLogWindow.Messenger;

namespace HappyCoding.ConsoleLogWindow.Application.UseCases;

public class StartProcessUseCase : IUseCase<ProcessInfo>
{
    private readonly IProcessRunner _processRunner;
    private readonly IFirLibMessagePublisher _messagePublisher;

    public StartProcessUseCase(
        IProcessRunner processRunner,
        IFirLibMessagePublisher messagePublisher)
    {
        _processRunner = processRunner;
        _messagePublisher = messagePublisher;
    }

    public async Task ExecuteAsync(ProcessInfo processToStart)
    {
        var processRunning = await _processRunner.IsProcessRunningAsync(processToStart);
        if (processRunning)
        {
            throw new ConsoleLogWindowApplicationException($"Process '{processToStart.Title}' is already running!");
        }

        var runningProcess = await _processRunner.StartProcessAsync(processToStart);
        if (runningProcess == null)
        {
            throw new ConsoleLogWindowApplicationException($"Unable to start process '{processToStart.Title}': Unknown error");
        }

        _messagePublisher.Publish(new ProcessStartedMessage()
        { 
            ProcessInfo = processToStart,
            Process = runningProcess
        });
    }
}
