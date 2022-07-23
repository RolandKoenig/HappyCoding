using System.Collections.Immutable;
using HappyCoding.ConsoleLogWindow.Domain.Model;
using HappyCoding.ConsoleLogWindow.Domain.Ports;

namespace HappyCoding.ConsoleLogWindow.InMemoryProcessGroupRepository;

internal class InMemoryProcessGroupRepositoryImpl : IProcessGroupRepository
{
    private List<ProcessGroup> _processGroups;

    public InMemoryProcessGroupRepositoryImpl()
    {
        _processGroups = new List<ProcessGroup>();
    }

    /// <inheritdoc />
    public Task AddProcessGroupAsync(ProcessGroup processGroup)
    {
        _processGroups.Add(processGroup);
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public Task RemoveProcessGroupAsync(ProcessGroup processGroup)
    {
        _processGroups.Remove(processGroup);
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public Task<ImmutableArray<ProcessGroup>> GetAllProcessGroupsAsync()
    {
        return Task.FromResult(ImmutableArray.Create(
            _processGroups.ToArray()));
    }
}
