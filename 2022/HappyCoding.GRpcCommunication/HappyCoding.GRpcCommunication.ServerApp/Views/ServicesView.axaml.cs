using Avalonia.Controls;

namespace HappyCoding.GRpcCommunication.ServerApp.Views;
public partial class ServicesView : UserControl
{
    public ServicesView()
    {
        this.RegisterViewModelMessageHandler();
        this.InitializeComponent();
    }
}
