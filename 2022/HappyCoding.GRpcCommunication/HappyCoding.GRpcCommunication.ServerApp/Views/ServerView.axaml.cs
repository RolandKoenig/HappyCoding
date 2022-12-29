using Avalonia.Controls;

namespace HappyCoding.GRpcCommunication.ServerApp.Views;

public partial class ServerView : UserControl
{
    public ServerView()
    {
        this.RegisterViewModel();
        this.InitializeComponent();
    }
}
