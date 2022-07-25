using System;
using System.Threading.Tasks;
using HappyCoding.ConsoleLogWindow.Application.Model;

namespace HappyCoding.ConsoleLogWindow.Gui.Persistence;

internal class FileBasedPersistenceTarget : IPersistenceTarget
{
    private readonly string _filePath;

    public FileBasedPersistenceTarget(string filePath)
    {
        _filePath = filePath;
    }

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
