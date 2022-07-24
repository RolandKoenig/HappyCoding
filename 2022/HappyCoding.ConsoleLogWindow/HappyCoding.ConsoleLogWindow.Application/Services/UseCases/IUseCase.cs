namespace HappyCoding.ConsoleLogWindow.Application.Services.UseCases;

public interface IUseCase
{

}

public interface IUseCaseNoArg : IUseCase
{
    Task ExecuteAsync();
}

public interface IUseCase<T> : IUseCase
{
    Task ExecuteAsync(T arg0);
}
