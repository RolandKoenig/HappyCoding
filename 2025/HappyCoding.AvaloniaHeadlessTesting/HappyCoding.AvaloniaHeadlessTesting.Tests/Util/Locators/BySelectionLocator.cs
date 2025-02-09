using Avalonia;
using Avalonia.Controls;

namespace HappyCoding.AvaloniaHeadlessTesting.Tests.Util.Locators;

public class BySelectionLocator(ILocator source) : ILocator
{
    public IEnumerable<Visual> LocateAll()
    {
        foreach (var actVisual in LocatorUtil.QueryVisualChildrenDeep(source.LocateAll()))
        {
            if ((actVisual is ListBoxItem { IsSelected: true}) ||
                (actVisual is MenuItem { IsSelected: true}) ||
                (actVisual is TabItem { IsSelected: true}) ||
                (actVisual is ComboBoxItem { IsSelected: true}) ||
                (actVisual is TreeViewItem { IsSelected: true}) ||
                (actVisual is DataGridRow { IsSelected: true}))
                // There may be many more...
            {
                yield return actVisual;
            }
        }
    }
}

public static class BySelectionLocatorExtensions
{
    public static ILocator ThenBySelection(this ILocator locator)
        => new BySelectionLocator(locator);
    
    public static ILocator LocateBySelection(this Visual visual)
        => new BySelectionLocator(new ThisVisualLocator(visual));
}