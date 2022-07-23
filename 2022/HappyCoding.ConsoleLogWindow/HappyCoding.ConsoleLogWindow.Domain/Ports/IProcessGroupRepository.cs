using System.Collections.Immutable;
using HappyCoding.ConsoleLogWindow.Domain.Model;

namespace HappyCoding.ConsoleLogWindow.Domain.Ports;

public interface IProcessGroupRepository
{
    public Task AddProcessGroupAsync(ProcessGroup processGroup);

    public Task RemoveProcessGroupAsync(ProcessGroup processGroup);

    public Task<ImmutableArray<ProcessGroup>> GetAllProcessGroupsAsync();
}
