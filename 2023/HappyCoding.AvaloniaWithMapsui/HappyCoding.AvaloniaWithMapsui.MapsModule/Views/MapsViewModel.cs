using CommunityToolkit.Mvvm.Messaging;
using HappyCoding.AvaloniaWithMapsui.FilesModule.Interface;
using HappyCoding.AvaloniaWithMapsui.SelectionModule.Interface;
using HappyCoding.AvaloniaWithMapsui.Shared;

namespace HappyCoding.AvaloniaWithMapsui.MapsModule.Views;

internal class MapsViewModel : OwnViewModelBase, 
    IRecipient<GpxFileSelectionChangedMessage>,
    IRecipient<GpxFileLoadedMessage>
{
    private readonly IMessenger _messenger;
    private readonly IGpxFileRepositoryAdapter _gpxFileRepository;
    private readonly IGpxFileSelectionManager _selectionManager;

    private IMapsViewService? _mapsViewService;

    public MapsViewModel(
        IMessenger messenger, 
        IGpxFileRepositoryAdapter gpxFileRepository,
        IGpxFileSelectionManager selectionManager)
    {
        _messenger = messenger;
        _gpxFileRepository = gpxFileRepository;
        _selectionManager = selectionManager;
    }

    /// <inheritdoc />
    public void Receive(GpxFileSelectionChangedMessage message)
    {
        _mapsViewService?.SetSelectedGpxFile(
            _selectionManager.GetSelectedGpxFile());
    }

    /// <inheritdoc />
    public void Receive(GpxFileLoadedMessage message)
    {
        _mapsViewService?.SetAvailableGpxFiles(
            _gpxFileRepository.GetAllLoadedGpxFiles());
    }

    /// <inheritdoc />
    protected override void OnViewAttached()
    {
        base.OnViewAttached();

        if (_mapsViewService == null)
        {
            _mapsViewService = this.GetViewService<IMapsViewService>();
            _mapsViewService.RouteClicked += this.OnMapsViewService_RouteClicked;
            _mapsViewService.SetAvailableGpxFiles(
                _gpxFileRepository.GetAllLoadedGpxFiles());
        }
        
        _messenger.RegisterAll(this);
    }
    
    /// <inheritdoc />
    protected override void OnViewDetached()
    {
        base.OnViewDetached();

        if (_mapsViewService != null)
        {
            _mapsViewService.RouteClicked -= this.OnMapsViewService_RouteClicked;
            _mapsViewService = null;
        }
        
        _messenger.UnregisterAll(this);
    }

    private void OnMapsViewService_RouteClicked(object? sender, RouteClickedEventArgs e)
    {
        _selectionManager.SetSelectedGpxFile(e.ClickedRoute);
    }
}