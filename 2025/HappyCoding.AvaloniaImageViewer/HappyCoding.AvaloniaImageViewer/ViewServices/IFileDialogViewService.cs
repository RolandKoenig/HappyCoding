using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Platform.Storage;

namespace HappyCoding.AvaloniaImageViewer.ViewServices;

public interface IFileDialogViewService
{
    Task<IStorageFile?> OpenFileAsync(string title, IEnumerable<FilePickerFileType>? filter, CancellationToken cancellationToken);
}