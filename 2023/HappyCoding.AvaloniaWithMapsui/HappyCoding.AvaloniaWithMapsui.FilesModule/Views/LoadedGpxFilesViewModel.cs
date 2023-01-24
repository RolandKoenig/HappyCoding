using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using HappyCoding.AvaloniaWithMapsui.FilesModule.Interface;
using HappyCoding.AvaloniaWithMapsui.FilesModule.Services;
using HappyCoding.AvaloniaWithMapsui.SelectionModule.Interface;
using HappyCoding.AvaloniaWithMapsui.Shared;
using NSubstitute;
using RolandK.AvaloniaExtensions.ViewServices;

namespace HappyCoding.AvaloniaWithMapsui.FilesModule.Views;

internal partial class LoadedGpxFilesViewModel : OwnViewModelBase, 
    IRecipient<GpxFileLoadedMessage>,
    IRecipient<GpxFileSelectionChangedMessage>
{
    private readonly IMessenger _messenger;
    private readonly IGpxFileSelectionManager _selectionManager;
    private readonly GpxFileRepository _gpxFileRepository;

    [ObservableProperty]
    private List<LoadedGpxFileInfo> _loadedGpxFiles;

    [ObservableProperty]
    private LoadedGpxFileInfo? _selectedFile;

    public static LoadedGpxFilesViewModel DesignViewModel => new LoadedGpxFilesViewModel(
        Substitute.For<IMessenger>(),
        Substitute.For<IGpxFileSelectionManager>(),
        new GpxFileRepository(Substitute.For<IMessenger>()));

    public LoadedGpxFilesViewModel(
        IMessenger messenger,
        IGpxFileSelectionManager selectionManager,
        GpxFileRepository gpxFileRepository)
    {
        _messenger = messenger;
        _selectionManager = selectionManager;
        _gpxFileRepository = gpxFileRepository;

        this.LoadedGpxFiles = new List<LoadedGpxFileInfo>(
            gpxFileRepository.LoadedGpxFiles);

        messenger.RegisterAll(this);
    }

    [RelayCommand]
    public async Task LoadFileAsync()
    {
        var dlgService = this.GetViewService<IOpenFileViewService>();

        var selectedFile = await dlgService.ShowOpenFileDialogAsync(
            new FileDialogFilter[]
            {
                new("Gpx-File (*.gpx)", "gpx")
            },
            "Load gpx file");
        if (string.IsNullOrEmpty(selectedFile)) { return; }

        await _gpxFileRepository.LoadGpxFileAsync(selectedFile);
    }

    partial void OnSelectedFileChanged(LoadedGpxFileInfo? value)
    { 
        _selectionManager.SetSelectedGpxFile(value?.Contents);
    }

    /// <inheritdoc />
    public void Receive(GpxFileLoadedMessage message)
    {
        this.LoadedGpxFiles = new List<LoadedGpxFileInfo>(
            _gpxFileRepository.LoadedGpxFiles);

        this.SelectedFile = _gpxFileRepository.LoadedGpxFiles
            .FirstOrDefault(x => x.Contents == message.LoadedFile);
    }

    /// <inheritdoc />
    public void Receive(GpxFileSelectionChangedMessage message)
    {
        var selectedFile = _selectionManager.GetSelectedGpxFile();
        if (selectedFile == null)
        {
            this.SelectedFile = null;
        }
        else
        {
            this.SelectedFile = _gpxFileRepository.LoadedGpxFiles
                .FirstOrDefault(x => x.Contents == selectedFile);
        }
    }
}