using System.Threading;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HappyCoding.AvaloniaImageViewer.Util;
using HappyCoding.AvaloniaImageViewer.ViewServices;

namespace HappyCoding.AvaloniaImageViewer;

public partial class MainWindowViewModel : OwnViewModelBase
{
    [ObservableProperty]
    private Bitmap? _currentBitmap;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Title))]
    private IStorageFile? _currentFile;
    
    public string Title
    {
        get
        {
            var strBuilder = ThreadStaticStringBuilder.Instance;
            strBuilder.Clear();

            strBuilder.Append("🧪 RolandK ImageViewer (Prototype)");
            if (this.CurrentFile != null)
            {
                strBuilder.Append(" - ");
                strBuilder.Append(this.CurrentFile.Path.LocalPath);
            }
            
            return strBuilder.ToString();
        }
    }

    [RelayCommand]
    private async Task LoadImageAsync(CancellationToken cancellationToken)
    {
        var srvOpenFile = this.GetViewService<IFileDialogViewService>();

        var selectedFile = await srvOpenFile.OpenFileAsync(
            "Load Image",
            null,
            cancellationToken);
        if (selectedFile == null) { return; }

        await this.LoadImageAsync(selectedFile);
    }
    
    [RelayCommand]
    private async Task MovePreviousImageAsync(CancellationToken cancellationToken)
    {
        if (this.CurrentFile == null) { return; }

        var directory = await this.CurrentFile.GetParentAsync();
        if(directory == null) { return; }

        IStorageFile? previousFile = null;
        await foreach (var actItem in directory.GetItemsAsync().WithCancellation(cancellationToken))
        {
            if (actItem is not IStorageFile actFile)
            {
                continue;
            }
            
            if (actFile.Path == this.CurrentFile.Path)
            {
                if (previousFile != null)
                {
                    await this.LoadImageAsync(previousFile);
                }
                break;
            }

            previousFile = actFile;
        }
    }
    
    [RelayCommand]
    private async Task MoveNextImageAsync(CancellationToken cancellationToken)
    {
        if (this.CurrentFile == null) { return; }

        var directory = await this.CurrentFile.GetParentAsync();
        if(directory == null) { return; }

        var fileFound = false;
        await foreach (var actItem in directory.GetItemsAsync().WithCancellation(cancellationToken))
        {
            if (actItem is not IStorageFile actFile)
            {
                continue;
            }

            if (fileFound)
            {
                await this.LoadImageAsync(actFile);
                break;
            }
            
            if (actFile.Path == this.CurrentFile.Path)
            {
                fileFound = true;
            }
        }
    }
    
    [RelayCommand]
    private async Task AutoOrientAsync(CancellationToken cancellationToken)
    {
        var bitmap = this.CurrentBitmap;
        if (bitmap == null)
        {
            return;
        }
        
        
    }
    
    private async Task LoadImageAsync(IStorageFile file)
    {
        await using var inStream = await file.OpenReadAsync();
        var loadedBitmap = await Task.Run(() => new Bitmap(inStream));
        
        this.CurrentFile = file;
        this.CurrentBitmap = loadedBitmap;
    }
}