using System;
using Avalonia.Controls;

namespace HappyCoding.ConsoleLogWindow.Gui.Util.ViewServices;

public static class ViewServiceUtil
{
    public static T GetViewService<T>(this IViewServiceHost thisControl)
        where T : class
    {
        return (T)TryGetViewService(thisControl, typeof(T))!; 
    }

    public static T? TryGetViewService<T>(this IViewServiceHost thisControl)
        where T : class
    {
        return TryGetViewService(thisControl, typeof(T)) as T;
    }

    public static object? TryGetViewService(this IViewServiceHost thisControl, Type viewServiceType)
    {
        // Search within ViewServices collection
        var actParent = thisControl;
        object? result = null;
        while ((actParent != null) && 
               (result == null))
        {
            foreach (var actViewService in actParent.ViewServices)
            {
                // ReSharper disable once UseMethodIsInstanceOfType
                if (!viewServiceType.IsAssignableFrom(actViewService.GetType())) { continue; }

                result = actViewService;
            }

            actParent = actParent.ParentViewServiceHost;
        }
        return result;
    }

    public static IViewServiceHost? TryFindParentViewServiceHost(Control control)
    {
        var actParent = control.Parent;
        while (actParent != null)
        {
            if (actParent is IViewServiceHost actViewServiceHost)
            {
                return actViewServiceHost;
            }

            actParent = actParent.Parent;
        }

        return null;
    }
}