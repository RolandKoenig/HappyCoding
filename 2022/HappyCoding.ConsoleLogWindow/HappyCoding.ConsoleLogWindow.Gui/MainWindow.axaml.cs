using Avalonia.Controls;
using HappyCoding.ConsoleLogWindow.Gui.Util;

namespace HappyCoding.ConsoleLogWindow.Gui;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        this.CreateAndAttachViewModel<MainWindowViewModel>();
    }
}