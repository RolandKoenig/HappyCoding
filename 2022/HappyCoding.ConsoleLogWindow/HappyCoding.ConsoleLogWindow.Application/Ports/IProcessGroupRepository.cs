using System.Collections.Immutable;
using HappyCoding.ConsoleLogWindow.Application.Model;

namespace HappyCoding.ConsoleLogWindow.Application.Ports;

public interface IProcessGroupRepository
{
    public Task AddProcessGroupAsync(ProcessGroup processGroup);

    public Task RemoveProcessGroupAsync(ProcessGroup processGroup);

    public Task<ImmutableArray<ProcessGroup>> GetAllProcessGroupsAsync();
}
