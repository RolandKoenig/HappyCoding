using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Mapsui;
using Mapsui.Layers;
using Mapsui.Nts;
using Mapsui.Tiling;
using Mapsui.Styles;
using Mapsui.UI.Avalonia;
using RolandK.AvaloniaExtensions.Mvvm;
using RolandK.AvaloniaExtensions.ViewServices.Base;
using RolandK.Formats.Gpx;

namespace HappyCoding.AvaloniaWithMapsui.MapsModule.Views;

public partial class MapsView : MvvmUserControl, IMapsViewService
{
    private MapControl _mapControl;
    private MemoryLayer _lineStringLayerForAll;
    private MemoryLayer _lineStringLayerForSelection;
    
    /// <inheritdoc />
    public event EventHandler<ViewServiceRequestEventArgs>? ViewServiceRequest;
    
    public MapsView()
    {
        AvaloniaXamlLoader.Load(this);

        _mapControl = this.FindControl<MapControl>("MapControl");
        _lineStringLayerForAll = new MemoryLayer();
        _lineStringLayerForSelection = new MemoryLayer();
        
        _mapControl.Map!.Layers.Add(OpenStreetMap.CreateTileLayer());
        _mapControl.Map.Layers.Add(_lineStringLayerForAll);
        _mapControl.Map.Layers.Add(_lineStringLayerForSelection);
        _mapControl.Map.RotationLock = false;
        _mapControl.UnSnapRotationDegrees = 30;
        _mapControl.ReSnapRotationDegrees = 5;

        this.ViewServices.Add(this);
    }

    /// <inheritdoc />
    public void SetAvailableGpxFiles(IReadOnlyList<GpxFile> allGpxFiles)
    {
        _lineStringLayerForAll.Features = allGpxFiles
            .Select(actFile =>
            {
                return new GeometryFeature()
                {
                    Geometry = actFile.Tracks[0].Segments[0].Points.GpxWaypointsToMapsuiGeometry(),
                    Styles = new[] { GpxRenderingHelper.CreateLineStringStyle_Default() }
                };
            })
            .ToArray();
        
        _mapControl.RefreshGraphics();
    }

    /// <inheritdoc />
    public void SetSelectedGpxFile(GpxFile? selection)
    {
        if (selection == null)
        {
            _lineStringLayerForSelection.Features = Array.Empty<GeometryFeature>();
        }

        _lineStringLayerForSelection.Features = new GeometryFeature[]
        {
            new()
            {
                Geometry = selection.Tracks[0].Segments[0].Points.GpxWaypointsToMapsuiGeometry(),
                Styles = new[] { GpxRenderingHelper.CreateLineStringStyle_Selected() }
            }
        };
        
        _mapControl.RefreshGraphics();
    }
}