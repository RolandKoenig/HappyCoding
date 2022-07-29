using System;
using System.Collections.Generic;
using Avalonia.Controls;
using HappyCoding.ConsoleLogWindow.Gui.Util.ViewServices;
using HappyCoding.ConsoleLogWindow.Gui.ViewServices;

namespace HappyCoding.ConsoleLogWindow.Gui.Util;

public class ViewAdapter : IView
{
    private readonly IViewServiceHost? _viewServiceHost;
    private readonly Control _view;

    public ViewAdapter(Control view)
    {
        _view = view;
        _viewServiceHost = _view as IViewServiceHost;
    }

    /// <inheritdoc />
    public ICollection<IViewService> ViewServices =>
        _viewServiceHost?.ViewServices ?? Array.Empty<IViewService>();

    /// <inheritdoc />
    public IViewServiceHost? ParentViewServiceHost => _viewServiceHost != null ?
        _viewServiceHost.ParentViewServiceHost :
        ViewServiceUtil.TryFindParentViewServiceHost(_view);
}