using System.Collections.Generic;
using System.Threading.Tasks;
using HappyCoding.ConsoleLogWindow.Gui.Util.ViewServices;

namespace HappyCoding.ConsoleLogWindow.Gui.ViewServices.FileDialogs;

public interface ISaveFileViewService : IViewService
{
    Task<string?> ShowSaveFileDialogAsync(IEnumerable<FileDialogFilter> filters, string defaultExtension);
}