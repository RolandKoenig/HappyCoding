using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyCoding.ConsoleLogWindow.Application.Model;

public interface IPersistenceTarget
{
    Task StoreProcessGroupRepositoryAsync(ProcessGroupRepository repo);

    Task<ProcessGroupRepository> LoadProcessGroupRepositoryAsync();
}
