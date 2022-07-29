using System.Collections.Generic;
using Avalonia.Controls;
using HappyCoding.ConsoleLogWindow.Gui.Util;
using HappyCoding.ConsoleLogWindow.Gui.Util.ViewServices;
using HappyCoding.ConsoleLogWindow.Gui.ViewServices.FileDialogs;

namespace HappyCoding.ConsoleLogWindow.Gui;

public partial class MainWindow : Window, IViewServiceHost
{
    private ViewServiceContainer _viewServiceContainer;

    public MainWindow()
    {
        _viewServiceContainer = new ViewServiceContainer(this);

        InitializeComponent();

        this.RegisterViewServices();
        this.CreateAndAttachViewModel<MainWindowViewModel>();
    }

    /// <inheritdoc />
    public ICollection<IViewService> ViewServices => _viewServiceContainer.ViewServices;

    /// <inheritdoc />
    public IViewServiceHost? ParentViewServiceHost => null;

    private void RegisterViewServices()
    {
        this.ViewServices.Add(new OpenFileDialogService(this));
        this.ViewServices.Add(new SaveFileDialogService(this));
    }
}