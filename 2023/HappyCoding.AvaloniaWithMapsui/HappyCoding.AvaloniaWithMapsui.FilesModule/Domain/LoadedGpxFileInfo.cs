using RolandK.Formats.Gpx;

namespace HappyCoding.AvaloniaWithMapsui.FilesModule.Services;

internal class LoadedGpxFileInfo
{
    public string FileName { get; }
    
    public GpxFile Contents { get; }

    public LoadedGpxFileInfo(string fileName, GpxFile contents)
    {
        this.FileName = fileName;
        this.Contents = contents;
    }
}