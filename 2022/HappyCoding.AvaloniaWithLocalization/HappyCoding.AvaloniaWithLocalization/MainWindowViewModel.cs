using System.Threading;

namespace HappyCoding.AvaloniaWithLocalization;

public class MainWindowViewModel
{
    public string CurrentCultureStatusText => string.Format(
        MainWindowResources.CurrentCultureName,
        Thread.CurrentThread.CurrentCulture.DisplayName);

    public string CurrentUiCultureStatusText => string.Format(
        MainWindowResources.CurrentUiCultureName,
        Thread.CurrentThread.CurrentUICulture.DisplayName);
}
