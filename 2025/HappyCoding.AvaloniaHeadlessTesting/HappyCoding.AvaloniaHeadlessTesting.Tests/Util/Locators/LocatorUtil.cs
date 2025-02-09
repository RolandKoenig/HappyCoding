using Avalonia;
using Avalonia.VisualTree;

namespace HappyCoding.AvaloniaHeadlessTesting.Tests.Util.Locators;

public static class LocatorUtil
{
    public static IEnumerable<Visual> QueryVisualChildrenDeep(Visual rootNode)
    {
        foreach (var actChildVisual in rootNode.GetVisualChildren())
        {
            yield return actChildVisual;
            foreach (var actInnerChildControl in QueryVisualChildrenDeep(actChildVisual))
            {
                yield return actInnerChildControl;
            }
        }
    }
    
    public static IEnumerable<Visual> QueryVisualChildrenDeep(IEnumerable<Visual> rootNodes)
    {
        foreach (var actRootNode in rootNodes)
        {
            foreach (var actVisual in QueryVisualChildrenDeep(actRootNode))
            {
                yield return actVisual;
            }
        }
    }
    
    public static IEnumerable<Visual> QueryVisualChildrenDeepContainingSelf(IEnumerable<Visual> rootNodes)
    {
        foreach (var actRootNode in rootNodes)
        {
            yield return actRootNode;
            foreach (var actVisual in QueryVisualChildrenDeep(actRootNode))
            {
                yield return actVisual;
            }
        }
    }
}