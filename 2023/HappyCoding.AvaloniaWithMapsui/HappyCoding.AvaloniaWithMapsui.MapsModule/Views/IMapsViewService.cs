using RolandK.AvaloniaExtensions.ViewServices.Base;
using RolandK.Formats.Gpx;

namespace HappyCoding.AvaloniaWithMapsui.MapsModule.Views;

public interface IMapsViewService : IViewService
{
    event EventHandler<RouteClickedEventArgs> RouteClicked; 

    void SetAvailableGpxFiles(IReadOnlyList<GpxFile> allGpxFiles);
    
    void SetSelectedGpxFile(GpxFile? selection);
}