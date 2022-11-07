using System;
using System.Globalization;
using System.IO;
using System.Resources;
using Avalonia.Controls;

namespace HappyCoding.AvaloniaWithLocalization;
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        this.Activated += OnActivated;
    }

    private void OnActivated(object? sender, EventArgs e)
    {
        var assembly = this.GetType().Assembly;
        var satAssembly = assembly.GetSatelliteAssembly(new CultureInfo("de"));
        // using var fileStream = satAssembly.GetManifestResourceStream("HappyCoding.AvaloniaWithLocalization.DummyText.md");
        // using var streamReader = new StreamReader(fileStream);
        // var fullText = streamReader.ReadToEnd();

        // ResourceManager m = new ResourceManager("HappyCoding.AvaloniaWithLocalization", assembly);
        // var test = m.GetStream("DummyText.md");
    }
}
