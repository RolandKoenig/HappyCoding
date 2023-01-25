using RolandK.Formats.Gpx;

namespace HappyCoding.AvaloniaWithMapsui.MapsModule.Views;

public class RouteClickedEventArgs : EventArgs
{
    public GpxFile? ClickedRoute { get; }

    public RouteClickedEventArgs(GpxFile? clickedRoute)
    {
        this.ClickedRoute = clickedRoute;
    }
}