using CommunityToolkit.Mvvm.Messaging;
using HappyCoding.AvaloniaWithMapsui.FilesModule.Interface;
using RolandK.Formats.Gpx;

namespace HappyCoding.AvaloniaWithMapsui.FilesModule.Services;

internal class GpxFileRepository
{
    private readonly IMessenger _messenger;
    
    public List<LoadedGpxFileInfo> LoadedGpxFiles { get; } = new();

    public GpxFileRepository(IMessenger messenger)
    {
        _messenger = messenger;
    }

    public async Task<LoadedGpxFileInfo> LoadGpxFileAsync(string filePath)
    {
        var loadedFile = await GpxFile.LoadAsync(filePath);

        var loadedFileInfo = new LoadedGpxFileInfo(
            Path.GetFileName(filePath),
            loadedFile);
        this.LoadedGpxFiles.Add(loadedFileInfo);

        _messenger.Send(new GpxFileLoadedMessage(loadedFile));

        return loadedFileInfo;
    }
}