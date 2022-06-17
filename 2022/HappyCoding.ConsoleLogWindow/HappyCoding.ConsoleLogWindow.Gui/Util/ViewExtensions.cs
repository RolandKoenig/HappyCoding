using System;
using Microsoft.Extensions.DependencyInjection;
using Avalonia;
using Avalonia.Controls;

namespace HappyCoding.ConsoleLogWindow.Gui.Util;

internal static class ViewExtensions
{
    public static void CreateAndAttachViewModel<TViewModel>(this Control control)
        where TViewModel : ViewModelBase
    {
        if (Design.IsDesignMode)
        {
            return;
        }

        if (Application.Current is not App currentApp)
        {
            throw new InvalidOperationException("Application is not loaded correctly!");
        }

        var viewModel = currentApp.Services?.GetService<TViewModel>();
        if (viewModel == null)
        {
            throw new InvalidOperationException($"Unable to create view model of type {typeof(TViewModel).FullName}!");
        }

        var viewAdapter = new ViewAdapter(control);

        control.AttachedToLogicalTree += (_, _) => viewModel.ViewLoaded(viewAdapter);
        control.DetachedFromLogicalTree += (_, _) => viewModel.ViewUnloaded();
        control.DataContext = viewModel;
    }
}