using System;
using System.Collections.Generic;
using System.IO;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Themes.Fluent;
using Mapsui;
using Mapsui.Layers;
using Mapsui.Nts;
using Mapsui.Styles;
using Mapsui.Tiling;
using Mapsui.UI;
using RolandK.AvaloniaExtensions.FluentThemeDetection;
using RolandK.Formats.Gpx;

namespace HappyCoding.AvaloniaWithMapsui;

public partial class MainWindow : Window
{
    private MemoryLayer _lineStringLayer;

    public MainWindow()
    {
        this.InitializeComponent();

        _lineStringLayer = new MemoryLayer();

        MapControl.Map!.Layers.Add(OpenStreetMap.CreateTileLayer());
        MapControl.Map.Layers.Add(_lineStringLayer);
        MapControl.Map.RotationLock = false;
        MapControl.UnSnapRotationDegrees = 30;
        MapControl.ReSnapRotationDegrees = 5;
    }
    
    private void OnMnuSetThemeLight_Click(object? sender, RoutedEventArgs e)
    {
        Application.Current.TrySetFluentThemeMode(FluentThemeMode.Light);
    }
    
    private void OnMnuSetThemeDark_Click(object? sender, RoutedEventArgs e)
    {
        Application.Current.TrySetFluentThemeMode(FluentThemeMode.Dark);
    }

    private async void OnMnuLoadGpxFile_Click(object? sender, RoutedEventArgs e)
    {
        try
        {
            var dlgOpenFile = new OpenFileDialog();
            dlgOpenFile.Title = "Load GPX file";
            dlgOpenFile.AllowMultiple = false;
            dlgOpenFile.Filters = new List<FileDialogFilter>
            {
                new FileDialogFilter()
                {
                    Extensions = new List<string>() {"gpx"},
                    Name = "GPX file"
                }
            };

            var selectedFiles = await dlgOpenFile.ShowAsync(this);
            if (selectedFiles == null) { return; }
            if (selectedFiles.Length != 1) { return; }
            if (string.IsNullOrEmpty(selectedFiles[0])) { return; }
            if (!File.Exists(selectedFiles[0])) { return; }

            var gpxFile = await GpxFile.LoadAsync(selectedFiles[0]);
            var lineString = gpxFile.Tracks[0].Segments[0].Points.GpxWaypointsToMapsuiGeometry();
            _lineStringLayer.Features = new IFeature[]{ new GeometryFeature()
            {
                Geometry = lineString,
                Styles = new IStyle[]{ CreateLineStringStyle() }
            } };

            var minX = double.MaxValue;
            var minY = double.MaxValue;
            var maxX = double.MinValue;
            var maxY = double.MinValue;
            for (var loop = 0; loop < lineString.Count; loop++)
            {
                var actCoord = lineString[loop];

                if (minX > actCoord.X) { minX = actCoord.X; }
                if (minY > actCoord.Y) { minY = actCoord.Y; }

                if (maxX < actCoord.X) { maxX = actCoord.X; }
                if (maxY < actCoord.Y) { maxY = actCoord.Y; }
            }
            this.MapControl.Navigator!.NavigateTo(new MRect(minX, minY, maxX, maxY));
        }
        catch (Exception)
        {
            // Ignore
        }
    }

    public static IStyle CreateLineStringStyle()
    {
        return new VectorStyle
        {
            Fill = null,
            Outline = null,
#pragma warning disable CS8670 // Object or collection initializer implicitly dereferences possibly null member.
            Line = { Color = Color.Black, Width = 6 }
        };
    }
}
