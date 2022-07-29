using System;
using System.Collections.Generic;
using System.Text;

namespace HappyCoding.ConsoleLogWindow.Gui.Util.ViewServices;

public interface IViewServiceHost
{
    public ICollection<IViewService> ViewServices { get; }

    public IViewServiceHost? ParentViewServiceHost { get; }
}