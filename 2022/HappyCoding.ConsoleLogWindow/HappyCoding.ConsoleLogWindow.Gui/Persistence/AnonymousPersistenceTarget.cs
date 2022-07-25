using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HappyCoding.ConsoleLogWindow.Application.Model;

namespace HappyCoding.ConsoleLogWindow.Gui.Persistence;

internal class AnonymousPersistenceTarget : IPersistenceTarget
{
    /// <inheritdoc />
    public Task StoreProcessGroupRepositoryAsync(ProcessGroupRepository repo)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<ProcessGroupRepository> LoadProcessGroupRepositoryAsync()
    {
        throw new NotImplementedException();
    }
}
