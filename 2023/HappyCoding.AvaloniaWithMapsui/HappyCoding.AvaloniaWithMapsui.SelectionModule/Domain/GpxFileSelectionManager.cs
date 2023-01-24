using CommunityToolkit.Mvvm.Messaging;
using HappyCoding.AvaloniaWithMapsui.SelectionModule.Interface;
using RolandK.Formats.Gpx;

namespace HappyCoding.AvaloniaWithMapsui.SelectionModule.Domain;

internal class GpxFileSelectionManager : IGpxFileSelectionManager
{
    private readonly IMessenger _messenger;

    private GpxFile? _selectedGpxFile;
    
    public GpxFileSelectionManager(IMessenger messenger)
    {
        _messenger = messenger;
    }
    
    /// <inheritdoc />
    public void SetSelectedGpxFile(GpxFile? gpxFile)
    {
        if (_selectedGpxFile != gpxFile)
        {
            _selectedGpxFile = gpxFile;
            _messenger.Send<GpxFileSelectionChangedMessage>();
        }
    }

    /// <inheritdoc />
    public GpxFile? GetSelectedGpxFile()
    {
        return _selectedGpxFile;
    }
}