using System;
using System.Collections.Generic;
using System.Text;

namespace HappyCoding.ConsoleLogWindow.Gui.Util.ViewServices;

public interface IViewService
{
    event EventHandler<ViewServiceRequestEventArgs>? ViewServiceRequest;
}