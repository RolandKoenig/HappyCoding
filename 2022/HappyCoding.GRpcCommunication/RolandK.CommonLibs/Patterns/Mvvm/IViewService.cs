using System;
using System.Collections.Generic;
using System.Text;

namespace RolandK.Patterns.Mvvm;

public interface IViewService
{
    event EventHandler<ViewServiceRequestEventArgs>? ViewServiceRequest;
}