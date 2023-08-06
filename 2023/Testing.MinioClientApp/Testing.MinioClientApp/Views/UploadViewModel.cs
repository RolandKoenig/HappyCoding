using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using RolandK.AvaloniaExtensions.ViewServices;
using RolandK.AvaloniaExtensions.ViewServices.FileDialogs;
using RolandK.AvaloniaExtensions.ViewServices.MessageBox;
using Testing.MinioClientApp.MinioHandling;
using Testing.MinioClientApp.Util;

namespace Testing.MinioClientApp.Views;

public partial class UploadViewModel : OwnViewModelBase
{
    private readonly MinioInterface _minioInterface;

    public static UploadViewModel DesignViewModel { get; } = new UploadViewModel(null!);
    
    public UploadViewModel(MinioInterface minioInterface)
    {
        _minioInterface = minioInterface;
    }

    [RelayCommand]
    public async Task UploadFileAsync()
    {
        var srvOpenFile = this.GetViewService<IOpenFileViewService>();
        var srvMessageBox = this.GetViewService<IMessageBoxViewService>();

        var selectedFile = await srvOpenFile.ShowOpenFileDialogAsync(
            Array.Empty<FileDialogFilter>(),
            string.Empty);
        if (string.IsNullOrEmpty(selectedFile)) { return; }

        try
        {
            var fileStream = File.OpenRead(selectedFile);
            var objectName = Path.GetFileName(selectedFile);
            var writtenFile = await _minioInterface.WriteFile(
                objectName, 
                fileStream,
                CancellationToken.None);

            await srvMessageBox.ShowAsync(
                "Upload file",
                $"File '{objectName}' uploaded successfully!",
                MessageBoxButtons.Ok);
        }
        catch (Exception ex)
        {
            await srvMessageBox.ShowAsync(
                "Error",
                ex.ToString(),
                MessageBoxButtons.Ok);
        }
    }
}