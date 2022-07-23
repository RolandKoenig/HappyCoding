using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyCoding.ConsoleLogWindow.Domain.Model;

public class ProcessGroup
{
    public string Title { get; set; }

    public List<ProcessInfo> Processes { get; set; }

}
