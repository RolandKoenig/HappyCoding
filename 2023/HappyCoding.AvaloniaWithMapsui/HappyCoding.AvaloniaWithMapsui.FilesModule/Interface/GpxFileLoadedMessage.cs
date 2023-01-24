using RolandK.Formats.Gpx;

namespace HappyCoding.AvaloniaWithMapsui.FilesModule.Interface;

public class GpxFileLoadedMessage
{
    public GpxFile LoadedFile { get; }

    public GpxFileLoadedMessage(GpxFile loadedFile)
    {
        this.LoadedFile = loadedFile;
    }
}