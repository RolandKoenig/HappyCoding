using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Mapsui;
using Mapsui.Layers;
using Mapsui.Nts;
using Mapsui.Tiling;
using Mapsui.UI.Avalonia;
using RolandK.AvaloniaExtensions.Mvvm.Markup;
using RolandK.AvaloniaExtensions.ViewServices.Base;
using RolandK.Formats.Gpx;

namespace HappyCoding.AvaloniaWithMapsui.MapsModule.Views;

public partial class MapsView : MvvmUserControl, IMapsViewService
{
    private MapControl _mapControl;
    private MemoryLayer _lineStringLayerForAll;
    private MemoryLayer _lineStringLayerForSelection;
    
    private DateTimeOffset _lastPointerPressTimestamp = DateTimeOffset.MinValue;
    
    /// <inheritdoc />
    public event EventHandler<RouteClickedEventArgs>? RouteClicked;
    
    /// <inheritdoc />
    public event EventHandler<ViewServiceRequestEventArgs>? ViewServiceRequest;
    
    public MapsView()
    {
        AvaloniaXamlLoader.Load(this);

        _mapControl = this.FindControl<MapControl>("MapControl");
        _lineStringLayerForAll = new MemoryLayer();
        _lineStringLayerForAll.IsMapInfoLayer = true;
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
                return new GeometryFeatureWithMetadata()
                {
                    Geometry = actFile.Tracks[0].Segments[0].Points.GpxWaypointsToMapsuiGeometry(),
                    Styles = new[] { GpxRenderingHelper.CreateLineStringStyle_Default() },
                    Route = actFile
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
        else
        {
            _lineStringLayerForSelection.Features = new GeometryFeatureWithMetadata[]
            {
                new()
                {
                    Geometry = selection.Tracks[0].Segments[0].Points.GpxWaypointsToMapsuiGeometry(),
                    Styles = new[] { GpxRenderingHelper.CreateLineStringStyle_Selected() },
                    Route = selection
                }
            };
        }
        
        _mapControl.RefreshGraphics();
    }

    private void OnMapControl_PointerPressed(object? sender, PointerPressedEventArgs e)
    {
        _lastPointerPressTimestamp = DateTimeOffset.UtcNow;
    }
    
    private void OnMapControl_PointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        if (DateTimeOffset.UtcNow - _lastPointerPressTimestamp > TimeSpan.FromMilliseconds(300)) { return; }

        if (e.InitialPressMouseButton == MouseButton.Right)
        {
            this.RouteClicked?.Invoke(this, new RouteClickedEventArgs(null));
            return;
        }

        if (e.InitialPressMouseButton == MouseButton.Left)
        {
            var mousePosition = e.GetCurrentPoint(_mapControl);
        
            var clickInfo = _mapControl.GetMapInfo(
                new MPoint(mousePosition.Position.X, mousePosition.Position.Y),
                3);
            if (clickInfo.Feature is GeometryFeatureWithMetadata featureWithMetadata)
            {
                this.RouteClicked?.Invoke(this, new RouteClickedEventArgs(featureWithMetadata.Route));
            }
            else
            {
                this.RouteClicked?.Invoke(this, new RouteClickedEventArgs(null));
            }
        }
    }

    private void OnMapControl_PointerMoved(object? sender, PointerEventArgs e)
    {
        var mousePosition = e.GetCurrentPoint(_mapControl);
        
        var mouseLocationInfo = _mapControl.GetMapInfo(
            new MPoint(mousePosition.Position.X, mousePosition.Position.Y),
            3);

        _mapControl.Cursor = mouseLocationInfo.Feature != null
            ? new Cursor(StandardCursorType.Hand)
            : Cursor.Default;
    }
}