using Avalonia.Controls;
using Mapsui.Tiling;

namespace HappyCoding.AvaloniaWithMapsui;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        this.InitializeComponent();

        MapControl.Map!.Layers.Add(OpenStreetMap.CreateTileLayer());
        MapControl.Map.RotationLock = false;
        MapControl.UnSnapRotationDegrees = 30;
        MapControl.ReSnapRotationDegrees = 5;
    }
}
