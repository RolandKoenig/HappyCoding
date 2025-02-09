using Avalonia;
using HappyCoding.AvaloniaHeadlessTesting.Toolkit;

namespace HappyCoding.AvaloniaHeadlessTesting.Tests.Util.Locators;

public class ByTestIdLocator(ILocator source, string testId) : ILocator
{
    public IEnumerable<Visual> LocateAll()
    {
        foreach (var actVisual in LocatorUtil.QueryVisualChildrenDeep(source.LocateAll()))
        {
            if (actVisual.GetValue(TestProperties.TestIdProperty) == testId)
            {
                yield return actVisual;
            }
        }
    }
}

public static class ByTestIdLocatorExtensions
{
    public static ILocator ThenByTestId(this ILocator locator, string testId)
        => new ByTestIdLocator(locator, testId);
    
    public static ILocator LocateByTestId(this Visual visual, string testId)
        => new ByTestIdLocator(new ThisVisualLocator(visual), testId);
}