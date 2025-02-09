using Avalonia;

namespace HappyCoding.AvaloniaHeadlessTesting.Tests.Util.Locators;

public interface ILocator
{
    IEnumerable<Visual> LocateAll();
}