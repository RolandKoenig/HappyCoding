using Avalonia;

namespace HappyCoding.AvaloniaHeadlessTesting.Tests.Util.Locators;

public class ByNameLocator(ILocator source, string name) : ILocator
{
    public IEnumerable<Visual> LocateAll()
    {
        foreach (var actVisual in LocatorUtil.QueryVisualChildrenDeep(source.LocateAll()))
        {
            if (actVisual.Name == name)
            {
                yield return actVisual;
            }
        }
    }
}

public static class ByNameLocatorExtensions
{
    public static ILocator ThenByName(this ILocator locator, string name)
        => new ByNameLocator(locator, name);
    
    public static ILocator LocateByName(this Visual visual, string name)
        => new ByNameLocator(new ThisVisualLocator(visual), name);
}