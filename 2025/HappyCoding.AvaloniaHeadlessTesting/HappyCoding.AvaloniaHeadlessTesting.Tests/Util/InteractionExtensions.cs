using Avalonia;
using Avalonia.Controls;
using Avalonia.Headless;
using Avalonia.Input;

namespace HappyCoding.AvaloniaHeadlessTesting.Tests.Util;

public static class InteractionExtensions
{
    public static void SimulateClick(this Visual visual, TopLevel topLevel)
    {
        var pointOnWindow = visual.TranslatePoint(new Point(2, 2), topLevel);
        topLevel.MouseDown(pointOnWindow!.Value, MouseButton.Left);
        topLevel.MouseUp(pointOnWindow!.Value, MouseButton.Left);
    }
}