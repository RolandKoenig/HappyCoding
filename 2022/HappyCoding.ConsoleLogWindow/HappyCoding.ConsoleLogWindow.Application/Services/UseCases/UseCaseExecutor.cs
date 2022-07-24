using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyCoding.ConsoleLogWindow.Application.Services.UseCases;

internal class UseCaseExecutor : IUseCaseExecutor
{
    /// <inheritdoc />
    public void ExecuteUseCaseAsync<T>() where T : IUseCaseNoArg
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task ExecuteUseCaseAsync<T, TArg0>(TArg0 arg0) where T : IUseCase<TArg0>
    {
        throw new NotImplementedException();
    }
}
