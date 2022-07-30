namespace HappyCoding.ConsoleLogWindow.Application.Services.UseCaseExecution;

public interface IUseCase
{

}

public interface IUseCaseNoArg : IUseCase
{
    Task ExecuteAsync();
}

public interface IUseCase<in TArg0> : IUseCase
{
    Task ExecuteAsync(TArg0 arg0);
}

public interface IUseCase<in TArg0, in TArg1> : IUseCase
{
    Task ExecuteAsync(TArg0 arg0, TArg1 arg1);
}

