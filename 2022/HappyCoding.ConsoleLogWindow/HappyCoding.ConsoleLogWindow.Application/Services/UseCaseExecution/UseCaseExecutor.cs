using System.Collections.Concurrent;
using Microsoft.Extensions.DependencyInjection;

namespace HappyCoding.ConsoleLogWindow.Application.Services.UseCaseExecution;

public class UseCaseExecutor : IUseCaseExecutor
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ConcurrentQueue<Func<Task>> _useCaseQueue;

    private bool _isExecuting;
    private object _isExecutingLock;

    public UseCaseExecutor(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _useCaseQueue = new ConcurrentQueue<Func<Task>>();

        _isExecuting = false;
        _isExecutingLock = new object();
    }

    private async void Trigger()
    {
        Func<Task>? nextOneToExecute = null;
        
        lock (_isExecutingLock)
        {
            if (_isExecuting) { return; }

            _isExecuting = _useCaseQueue.TryDequeue(out nextOneToExecute);
            if (!_isExecuting) { return; }
        }

        while (nextOneToExecute != null)
        {
            try
            {
                await nextOneToExecute();
            }
            catch (Exception ex)
            {
                // TODO: Handle error from use case
            }
            
            lock (_isExecutingLock)
            {
                _isExecuting = _useCaseQueue.TryDequeue(out nextOneToExecute);
                if (!_isExecuting) { return; }
            }
        }
    }

    /// <inheritdoc />
    public Task ExecuteUseCaseAsync<T>() where T : IUseCaseNoArg
    {
        var taskCompletionSource = new TaskCompletionSource<object?>();

        var useCase = _serviceProvider.GetRequiredService<T>();
        _useCaseQueue.Enqueue(
            () =>
            {
                try
                {
                    var result = useCase.ExecuteAsync();
                    taskCompletionSource.SetResult(null);
                    return result;
                }
                catch (Exception ex)
                {
                    taskCompletionSource.SetException(ex);
                    throw;
                }
            });

        this.Trigger();
        
        return taskCompletionSource.Task;
    }

    /// <inheritdoc />
    public Task ExecuteUseCaseAsync<T, TArg0>(TArg0 arg0) where T : IUseCase<TArg0>
    {
        var taskCompletionSource = new TaskCompletionSource<object?>();

        var useCase = _serviceProvider.GetRequiredService<T>();
        _useCaseQueue.Enqueue(
            () =>
            {
                try
                {
                    var result= useCase.ExecuteAsync(arg0);
                    taskCompletionSource.SetResult(null);
                    return result;
                }
                catch (Exception ex)
                {
                    taskCompletionSource.SetException(ex);
                    throw;
                }
            });
        
        this.Trigger();

        return taskCompletionSource.Task;
    }

    /// <inheritdoc />
    public Task ExecuteUseCaseAsync<T, TArg0, TArg1>(TArg0 arg0, TArg1 arg1) where T : IUseCase<TArg0, TArg1>
    {
        var taskCompletionSource = new TaskCompletionSource<object?>();

        var useCase = _serviceProvider.GetRequiredService<T>();
        _useCaseQueue.Enqueue(
            () =>
            {
                try
                {
                    var result= useCase.ExecuteAsync(arg0, arg1);
                    taskCompletionSource.SetResult(null);
                    return result;
                }
                catch (Exception ex)
                {
                    taskCompletionSource.SetException(ex);
                    throw;
                }
            });
        
        this.Trigger();

        return taskCompletionSource.Task;
    }
}
