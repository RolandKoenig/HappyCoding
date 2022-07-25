using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyCoding.ConsoleLogWindow.Application.Model;

public class ProcessGroupRepository
{
    public ObservableCollection<ProcessGroup> ProcessGroups { get; set; } = new();

    public ProcessGroupRepository()
    { 
        
    }
}
