using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HappyCoding.AvaloniaImageViewer.Util;
using HappyCoding.AvaloniaImageViewer.ViewServices;
using ImageMagick;

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
                strBuilder.Append(PathUtil.GetFileName(this.CurrentFile.Path));
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
    private async Task<bool> MovePreviousImageAsync(CancellationToken cancellationToken)
    {
        if (this.CurrentFile == null) { return false; }

        var directory = await this.CurrentFile.GetParentAsync();
        if(directory == null) { return false; }

        IStorageFile? previousFile = null;
        var fileChanged = false;
        await foreach (var actItem in directory.GetItemsAsync().OrderBy(x => x.Path.LocalPath).WithCancellation(cancellationToken))
        {
            if (actItem is not IStorageFile actFile) { continue; }
            if (!ImageUtil.IsSupportedImageFormat(actFile)){ continue; }
            
            if (actFile.Path == this.CurrentFile.Path)
            {
                if (previousFile != null)
                {
                    await this.LoadImageAsync(previousFile);
                    fileChanged = true;
                }
                break;
            }

            previousFile = actFile;
        }

        return fileChanged;
    }
    
    [RelayCommand]
    private async Task<bool> MoveNextImageAsync(CancellationToken cancellationToken)
    {
        if (this.CurrentFile == null) { return false; }

        var directory = await this.CurrentFile.GetParentAsync();
        if(directory == null) { return false; }

        var fileFound = false;
        var fileChanged = false;
        await foreach (var actItem in directory.GetItemsAsync().OrderBy(x => x.Path.LocalPath).WithCancellation(cancellationToken))
        {
            if (actItem is not IStorageFile actFile) { continue; }
            if (!ImageUtil.IsSupportedImageFormat(actFile)){ continue; }
            if (fileFound)
            {
                await this.LoadImageAsync(actFile);
                fileChanged = true;
                break;
            }
            
            if (actFile.Path == this.CurrentFile.Path)
            {
                fileFound = true;
            }
        }

        return fileChanged;
    }
    
    [RelayCommand]
    private async Task AutoOrientAsync(CancellationToken cancellationToken)
    {
        if(this.CurrentFile == null) { return; }

        await using var inStream = await this.CurrentFile.OpenReadAsync();
        await using var memStream = new MemoryStream((int)inStream.Length);
        await inStream.CopyToAsync(memStream, cancellationToken);
        inStream.Close();
        var memStreamBuffer = memStream.GetBuffer();
        
        using var image = await Task.Run(() =>
        {
            ReadOnlySpan<byte> memStreamSpan = memStreamBuffer;
            return new MagickImage(memStreamSpan);
        }, cancellationToken);
        
        image.AutoOrient();

        await using var outStream = await this.CurrentFile.OpenWriteAsync();
        await image.WriteAsync(outStream, cancellationToken);
        
        // ReSharper disable once DisposeOnUsingVariable
        await outStream.DisposeAsync();

        await this.LoadImageAsync(this.CurrentFile);
    }

    [RelayCommand]
    private async Task DeleteImageAsync(CancellationToken cancellationToken)
    {
        if(this.CurrentFile == null) { return; }

        var previousFile = this.CurrentFile;
        if (await MoveNextImageAsync(cancellationToken) ||
            await MovePreviousImageAsync(cancellationToken))
        {
            await previousFile.DeleteAsync();
        }
        else
        {
            // Last file in the current directory
            this.CurrentFile = null;
            this.CurrentBitmap = null;
            await previousFile.DeleteAsync();
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