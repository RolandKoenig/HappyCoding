using System;
using System.Collections.Generic;
using System.Text;

namespace HappyCoding.ConsoleLogWindow.Gui.Util.ViewServices;

public class ViewServiceRequestEventArgs
{
    public Type ViewServiceType { get; }

    public object? ViewService { get; set; }

    public ViewServiceRequestEventArgs(Type viewServiceType)
    {
        this.ViewServiceType = viewServiceType;
    }
}