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
        var mapsViewService = this.GetViewService<IMapsViewService>();
        
        mapsViewService.SetSelectedGpxFile(
            _selectionManager.GetSelectedGpxFile());
    }

    /// <inheritdoc />
    public void Receive(GpxFileLoadedMessage message)
    {
        var mapsViewService = this.GetViewService<IMapsViewService>();
        
        mapsViewService.SetAvailableGpxFiles(
            _gpxFileRepository.GetAllLoadedGpxFiles());
    }

    /// <inheritdoc />
    protected override void OnViewAttached()
    {
        base.OnViewAttached();
        
        _messenger.RegisterAll(this);
        
        // TODO: Workaround because of bug in RolandK.AvaloniaExtensions.Mvvm
        SynchronizationContext.Current.Post(
            _ =>
            {
                var mapsViewService = this.GetViewService<IMapsViewService>();
        
                mapsViewService.SetAvailableGpxFiles(
                    _gpxFileRepository.GetAllLoadedGpxFiles());
            },
            null);
    }

    /// <inheritdoc />
    protected override void OnViewDetached()
    {
        base.OnViewDetached();

        _messenger.UnregisterAll(this);
    }
}