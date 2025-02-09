using Avalonia;

namespace HappyCoding.AvaloniaHeadlessTesting.Tests.Util.Locators;

public class ByClassLocator(ILocator source, string className) : ILocator
{
    public IEnumerable<Visual> LocateAll()
    {
        foreach (var actVisual in LocatorUtil.QueryVisualChildrenDeep(source.LocateAll()))
        {
            if (actVisual.Classes.Contains(className))
            {
                yield return actVisual;
            }
        }
    }
}

public static class ByClassLocatorExtensions
{
    public static ILocator ThenByClass(this ILocator locator, string className)
        => new ByClassLocator(locator, className);
    
    public static ILocator LocateByClass(this Visual visual, string className)
        => new ByClassLocator(new ThisVisualLocator(visual), className);
}