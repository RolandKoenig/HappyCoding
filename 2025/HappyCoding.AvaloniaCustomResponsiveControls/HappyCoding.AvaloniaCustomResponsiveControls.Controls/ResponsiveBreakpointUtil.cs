namespace HappyCoding.AvaloniaCustomResponsiveControls.Controls;

public static class ResponsiveBreakpointUtil
{
    public static ResponsiveGridBreakpoint GetCurrentBreakpoint(double width)
    {
        return width switch
        {
            >= 1400d => ResponsiveGridBreakpoint.Xxl,
            >= 1200d => ResponsiveGridBreakpoint.Xl,
            >= 992 => ResponsiveGridBreakpoint.Lg,
            >= 768 => ResponsiveGridBreakpoint.Md,
            >= 576d => ResponsiveGridBreakpoint.Sm,
            _ => ResponsiveGridBreakpoint.Xs
        };
    }
}