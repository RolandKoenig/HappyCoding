using Mapsui.Nts;
using RolandK.Formats.Gpx;

namespace HappyCoding.AvaloniaWithMapsui.MapsModule.Views;

public class GeometryFeatureWithMetadata : GeometryFeature
{
    public GpxFile? Route { get; set; }
}