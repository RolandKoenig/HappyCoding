using RolandK.AvaloniaExtensions.ViewServices.Base;
using RolandK.Formats.Gpx;

namespace HappyCoding.AvaloniaWithMapsui.MapsModule.Views;

public interface IMapsViewService : IViewService
{
    void SetAvailableGpxFiles(IReadOnlyList<GpxFile> allGpxFiles);
    
    void SetSelectedGpxFile(GpxFile? selection);
}