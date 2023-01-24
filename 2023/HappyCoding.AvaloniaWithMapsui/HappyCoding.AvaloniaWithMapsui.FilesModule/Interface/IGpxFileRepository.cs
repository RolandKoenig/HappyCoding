using RolandK.Formats.Gpx;

namespace HappyCoding.AvaloniaWithMapsui.FilesModule.Interface;

public interface IGpxFileRepositoryAdapter
{
    IReadOnlyList<GpxFile> GetAllLoadedGpxFiles();
}