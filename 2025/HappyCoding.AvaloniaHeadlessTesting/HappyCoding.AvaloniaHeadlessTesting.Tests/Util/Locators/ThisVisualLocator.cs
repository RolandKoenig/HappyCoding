using Avalonia;

namespace HappyCoding.AvaloniaHeadlessTesting.Tests.Util.Locators;

public class ThisVisualLocator : ILocator
{
    private readonly Visual _visual;
    
    public ThisVisualLocator(Visual visual)
    {
        _visual = visual;
    }
    
    public IEnumerable<Visual> LocateAll()
    {
        yield return _visual;
    }
}