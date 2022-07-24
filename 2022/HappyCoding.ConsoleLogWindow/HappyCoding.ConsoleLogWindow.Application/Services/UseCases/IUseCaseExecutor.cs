namespace HappyCoding.ConsoleLogWindow.Application.Services.UseCases;

public interface IUseCaseExecutor
{
    public Task ExecuteUseCaseAsync<T>()
        where T : IUseCaseNoArg;

    public Task ExecuteUseCaseAsync<T, TArg0>(TArg0 arg0)
        where T : IUseCase<TArg0>;
}
