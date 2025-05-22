using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using RolandK.AvaloniaExtensions.ViewServices.Base;

namespace HappyCoding.AvaloniaImageViewer.ViewServices.Default;

public class DefaultFileDialogViewService : ViewServiceBase, IFileDialogViewService
{
    private readonly Window _parentWindow;
    
    public DefaultFileDialogViewService(Window parentWindow)
    {
        _parentWindow = parentWindow;
    }
    
    public async Task<IStorageFile?> OpenFileAsync(
        string title, 
        IEnumerable<FilePickerFileType>? filters, 
        CancellationToken cancellationToken)
    {
        var filePickerOptions = new FilePickerOpenOptions();
        filePickerOptions.AllowMultiple = false;
        filePickerOptions.FileTypeFilter = filters?.ToList();
        
        var selectedFiles = await _parentWindow.StorageProvider.OpenFilePickerAsync(filePickerOptions);
        
        if (selectedFiles.Count == 0)
        {
            return null;
        }
        else
        {
            return selectedFiles[0];
        }
    }
}