using System.Collections.Generic;
using System.Threading.Tasks;
using HappyCoding.ConsoleLogWindow.Gui.Util.ViewServices;

namespace HappyCoding.ConsoleLogWindow.Gui.ViewServices.FileDialogs;

public interface IOpenFileViewService : IViewService
{
    Task<string?> ShowOpenFileDialogAsync(IEnumerable<FileDialogFilter> filters, string title);

    Task<string[]?> ShowOpenMultipleFilesDialogAsync(IEnumerable<FileDialogFilter> filters, string title);
}