namespace HappyCoding.ConsoleLogWindow.Application.UseCases;

public interface IUseCase<T>
{
    Task ExecuteAsync(T arg0);
}
