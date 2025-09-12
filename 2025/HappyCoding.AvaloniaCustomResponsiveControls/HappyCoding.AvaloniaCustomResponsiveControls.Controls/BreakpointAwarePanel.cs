using Avalonia;
using Avalonia.Controls;

namespace HappyCoding.AvaloniaCustomResponsiveControls.Controls;

public class BreakpointAwarePanel : Panel
{
    // ReSharper disable once MemberCanBePrivate.Global
    public static readonly StyledProperty<double> BreakpointSmProperty =
        AvaloniaProperty.Register<BreakpointAwarePanel, double>(
            nameof(BreakpointSm), 
            defaultValue: 576d);
    public double BreakpointSm
    {
        get => this.GetValue(BreakpointSmProperty);
        set => this.SetValue(BreakpointSmProperty, value);
    }
    
    // ReSharper disable once MemberCanBePrivate.Global
    public static readonly StyledProperty<double> BreakpointMdProperty =
        AvaloniaProperty.Register<BreakpointAwarePanel, double>(
            nameof(BreakpointMd), 
            defaultValue: 768d);
    public double BreakpointMd
    {
        get => this.GetValue(BreakpointMdProperty);
        set => this.SetValue(BreakpointMdProperty, value);
    }
    
    // ReSharper disable once MemberCanBePrivate.Global
    public static readonly StyledProperty<double> BreakpointLgProperty =
        AvaloniaProperty.Register<BreakpointAwarePanel, double>(
            nameof(BreakpointLg), 
            defaultValue: 992d);
    public double BreakpointLg
    {
        get => this.GetValue(BreakpointLgProperty);
        set => this.SetValue(BreakpointLgProperty, value);
    }
    
    // ReSharper disable once MemberCanBePrivate.Global
    public static readonly StyledProperty<double> BreakpointXlProperty =
        AvaloniaProperty.Register<BreakpointAwarePanel, double>(
            nameof(BreakpointXl), 
            defaultValue: 1200d);
    public double BreakpointXl
    {
        get => this.GetValue(BreakpointXlProperty);
        set => this.SetValue(BreakpointXlProperty, value);
    }

    // ReSharper disable once MemberCanBePrivate.Global
    public static readonly StyledProperty<double> BreakpointXxlProperty =
        AvaloniaProperty.Register<BreakpointAwarePanel, double>(
            nameof(BreakpointXxl), 
            defaultValue: 1400d);
    public double BreakpointXxl
    {
        get => this.GetValue(BreakpointXxlProperty);
        set => this.SetValue(BreakpointXxlProperty, value);
    }
    
    private ResponsiveGridBreakpoint _currentBreakpoint = ResponsiveGridBreakpoint.Sm;
    
    // ReSharper disable once MemberCanBeProtected.Global
    /// <summary>
    /// Gets the breakpoint calculated in the last measure pass.
    /// </summary>
    public ResponsiveGridBreakpoint CurrentBreakpoint => _currentBreakpoint;
    
    protected override Size MeasureCore(Size availableSize)
    {
        _currentBreakpoint = CalculateBreakpoint(availableSize.Width);
        return base.MeasureCore(availableSize);
    }
    
    // ReSharper disable once MemberCanBePrivate.Global
    /// <summary>
    /// Calculates the breakpoint for the given width.
    /// </summary>
    protected ResponsiveGridBreakpoint CalculateBreakpoint(double width)
    {
        if (width >= this.BreakpointXxl)
        {
            return ResponsiveGridBreakpoint.Xxl;
        }

        if (width >= this.BreakpointXl)
        {
            return ResponsiveGridBreakpoint.Xl;
        }

        if (width >= this.BreakpointLg)
        {
            return ResponsiveGridBreakpoint.Lg;
        }

        if (width >= this.BreakpointMd)
        {
            return ResponsiveGridBreakpoint.Md;
        }

        if (width >= this.BreakpointSm)
        {
            return ResponsiveGridBreakpoint.Sm;
        }

        return ResponsiveGridBreakpoint.Xs;
    }
}