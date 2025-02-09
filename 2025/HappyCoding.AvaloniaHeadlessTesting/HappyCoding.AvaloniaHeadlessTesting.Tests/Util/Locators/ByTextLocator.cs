using Avalonia;
using Avalonia.Controls;

namespace HappyCoding.AvaloniaHeadlessTesting.Tests.Util.Locators;

public class ByTextLocator(ILocator source, string text) : ILocator
{
    public IEnumerable<Visual> LocateAll()
    {
        foreach (var actVisual in LocatorUtil.QueryVisualChildrenDeep(source.LocateAll()))
        {
            if ((actVisual is TextBlock textBlock && textBlock.Text == text) ||
                (actVisual is TextBox textBox && textBox.Text == text))
            {
                yield return actVisual;
            }
        }
    }
}

public static class ByTextLocatorExtensions
{
    public static ILocator ThenByText(this ILocator locator, string text)
        => new ByTextLocator(locator, text);
    
    public static ILocator LocateByText(this Visual visual, string text)
        => new ByTextLocator(new ThisVisualLocator(visual), text);
}