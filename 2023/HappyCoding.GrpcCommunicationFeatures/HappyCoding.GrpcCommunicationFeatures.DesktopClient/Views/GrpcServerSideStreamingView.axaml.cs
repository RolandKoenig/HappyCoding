using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace HappyCoding.GrpcCommunicationFeatures.DesktopClient.Views;

public partial class GrpcServerSideStreamingView : UserControl
{
    public GrpcServerSideStreamingView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}