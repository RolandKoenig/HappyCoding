using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace HappyCoding.GrpcCommunicationFeatures.DesktopClient.Views;

public partial class GrpcBidirectionalStreamingView : UserControl
{
    public GrpcBidirectionalStreamingView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}