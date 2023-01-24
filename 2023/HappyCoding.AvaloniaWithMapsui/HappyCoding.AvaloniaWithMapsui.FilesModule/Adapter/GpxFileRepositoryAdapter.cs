using HappyCoding.AvaloniaWithMapsui.FilesModule.Interface;
using HappyCoding.AvaloniaWithMapsui.FilesModule.Services;
using RolandK.Formats.Gpx;

namespace HappyCoding.AvaloniaWithMapsui.FilesModule.Adapter;

internal class GpxFileRepositoryAdapter : IGpxFileRepositoryAdapter
{
    private readonly GpxFileRepository _gpxFileRepository;

    public GpxFileRepositoryAdapter(GpxFileRepository gpxFileRepository)
    {
        _gpxFileRepository = gpxFileRepository;
    }
    
    /// <inheritdoc />
    public IReadOnlyList<GpxFile> GetAllLoadedGpxFiles()
    {
        return _gpxFileRepository.LoadedGpxFiles
            .Select(x => x.Contents)
            .ToArray();
    }
}