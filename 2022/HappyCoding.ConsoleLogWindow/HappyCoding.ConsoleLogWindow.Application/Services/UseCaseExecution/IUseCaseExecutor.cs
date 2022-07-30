namespace HappyCoding.ConsoleLogWindow.Application.Services.UseCaseExecution;

public interface IUseCaseExecutor
{
    public Task ExecuteUseCaseAsync<T>()
        where T : IUseCaseNoArg;

    public Task ExecuteUseCaseAsync<T, TArg0>(TArg0 arg0)
        where T : IUseCase<TArg0>;

    public Task ExecuteUseCaseAsync<T, TArg0, TArg1>(TArg0 arg0, TArg1 arg1)
        where T : IUseCase<TArg0, TArg1>;
}
