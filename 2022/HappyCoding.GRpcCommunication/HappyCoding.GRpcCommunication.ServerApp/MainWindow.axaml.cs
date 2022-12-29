using Avalonia.Controls;
using HappyCoding.GRpcCommunication.ServerApp.Views;

namespace HappyCoding.GRpcCommunication.ServerApp;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        this.RegisterViewModel();
        this.InitializeComponent();
    }
}
