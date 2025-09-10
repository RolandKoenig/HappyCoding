using Avalonia;
using Avalonia.Controls;

namespace HappyCoding.AvaloniaCustomResponsiveControls.Controls;

public class ResponsiveGrid : Panel
{
    public static readonly AttachedProperty<int> ColumnsProperty =
        AvaloniaProperty.RegisterAttached<ResponsiveGrid, Control, int>(
            "Columns",
            defaultValue: 0,
            validate: v => v is >= 0 and <= 12);
    public static readonly AttachedProperty<int> ColumnsSmProperty =
        AvaloniaProperty.RegisterAttached<ResponsiveGrid, Control, int>(
            "ColumnSm",
            defaultValue: 0,
            validate: v => v is >= 0 and <= 12);
    public static readonly AttachedProperty<int> ColumnsMdProperty =
        AvaloniaProperty.RegisterAttached<ResponsiveGrid, Control, int>(
            "ColumnsMd",
            defaultValue: 0,
            validate: v => v is >= 0 and <= 12);
    public static readonly AttachedProperty<int> ColumnsLgProperty =
        AvaloniaProperty.RegisterAttached<ResponsiveGrid, Control, int>(
            "ColumnsLg",
            defaultValue: 0,
            validate: v => v is >= 0 and <= 12);
    public static readonly AttachedProperty<int> ColumnsXlProperty =
        AvaloniaProperty.RegisterAttached<ResponsiveGrid, Control, int>(
            "ColumnsXl",
            defaultValue: 0,
            validate: v => v is >= 0 and <= 12);
    public static readonly AttachedProperty<int> ColumnsXxlProperty =
        AvaloniaProperty.RegisterAttached<ResponsiveGrid, Control, int>(
            "ColumnsXxl",
            defaultValue: 0,
            validate: v => v is >= 0 and <= 12);
    
    /// <summary>
    /// Gets the breakpoint that was calculated in the last measure pass.
    /// </summary>
    public ResponsiveGridBreakpoint CurrentBreakpoint
    {
        get;
        private set;
    }

    public static void SetColumns(AvaloniaObject element, int value)
    {
        element.SetValue(ColumnsProperty, value);
    }
    
    public static int GetColumns(AvaloniaObject element)
    {
        return element.GetValue(ColumnsProperty);
    }
    
    public static void SetColumnsSm(AvaloniaObject element, int value)
    {
        element.SetValue(ColumnsSmProperty, value);
    }
    
    public static int GetColumnsSm(AvaloniaObject element)
    {
        return element.GetValue(ColumnsSmProperty);
    }
    
    public static void SetColumnsMd(AvaloniaObject element, int value)
    {
        element.SetValue(ColumnsMdProperty, value);
    }
    
    public static int GetColumnsMd(AvaloniaObject element)
    {
        return element.GetValue(ColumnsMdProperty);
    }
    
    public static void SetColumnsLg(AvaloniaObject element, int value)
    {
        element.SetValue(ColumnsLgProperty, value);
    }
    
    public static int GetColumnsLg(AvaloniaObject element)
    {
        return element.GetValue(ColumnsLgProperty);
    }
    
    public static void SetColumnsXl(AvaloniaObject element, int value)
    {
        element.SetValue(ColumnsXlProperty, value);
    }
    
    public static int GetColumnsXl(AvaloniaObject element)
    {
        return element.GetValue(ColumnsXlProperty);
    }
    
    public static void SetColumnsXxl(AvaloniaObject element, int value)
    {
        element.SetValue(ColumnsXxlProperty, value);
    }
    
    public static int GetColumnsXxl(AvaloniaObject element)
    {
        return element.GetValue(ColumnsXxlProperty);
    }
    
    protected override Size MeasureOverride(Size availableSize)
    {
        this.CurrentBreakpoint = GetCurrentBreakpoint(availableSize.Width);
        
        var singleColumnWidth = double.IsFinite(availableSize.Width)
            ? availableSize.Width / 12.0
            : double.PositiveInfinity;
        var fullBottomLine = 0d;
        var currentColumnStartIndex = 0;

        var actLineBottomLine = 0d;
        var actRowDesiredWith = 0d;
        var maxRowDesiredWith = 0d;
        foreach (var actChild in Children)
        {
            var actChildColumns = GetColumnCount(actChild, CurrentBreakpoint);
            if (currentColumnStartIndex + actChildColumns > 12)
            {
                if (actRowDesiredWith > maxRowDesiredWith)
                {
                    maxRowDesiredWith = actRowDesiredWith;
                }
                
                // Proceed to new line
                fullBottomLine += actLineBottomLine;
                actLineBottomLine = 0d;
                currentColumnStartIndex = 0;
                actRowDesiredWith = 0d;
            }
            
            actChild.Measure(new Size(
                singleColumnWidth * actChildColumns, 
                availableSize.Height));
            
            var actChildDesiredSize = actChild.DesiredSize;
            if (actChildDesiredSize.Height > actLineBottomLine)
            {
                actLineBottomLine = actChildDesiredSize.Height;
            }
            actRowDesiredWith += actChildDesiredSize.Width;

            currentColumnStartIndex += actChildColumns;
        }
        fullBottomLine += actLineBottomLine;
        
        return new Size(
            double.IsFinite(availableSize.Width)
                ? availableSize.Width
                : maxRowDesiredWith, 
            fullBottomLine);
    }

    protected override Size ArrangeOverride(Size finalSize)
    {
        var singleColumnWidth = finalSize.Width / 12.0;
        
        var fullBottomLine = 0d;
        var currentColumnStartIndex = 0;

        var actLineBottomLine = 0d;
        var currentLineContents = new List<ResponsiveGridRowContent>(12);
        foreach (var actChild in Children)
        {
            var actChildColumnCount = GetColumnCount(actChild, CurrentBreakpoint);
            if (currentColumnStartIndex + actChildColumnCount > 12)
            {
                // Arrange all childs in the last row
                ArrangeChilds(
                    currentLineContents, fullBottomLine, actLineBottomLine, singleColumnWidth);
                currentLineContents.Clear();
                
                // Proceed to new line
                fullBottomLine += actLineBottomLine;
                actLineBottomLine = 0d;
                currentColumnStartIndex = 0;
            }
            
            var actChildDesiredSize = actChild.DesiredSize;
            if (actChildDesiredSize.Height > actLineBottomLine)
            {
                actLineBottomLine = actChildDesiredSize.Height;
            }

            currentColumnStartIndex += actChildColumnCount;
            
            currentLineContents.Add(new ResponsiveGridRowContent(
                actChild, actChildColumnCount));
        }

        if (currentLineContents.Count > 0)
        {
            // Arrange all childs in the last row
            ArrangeChilds(
                currentLineContents, fullBottomLine, actLineBottomLine, singleColumnWidth);
            currentLineContents.Clear();
            
            // Close up
            fullBottomLine += actLineBottomLine;
        }
        
        return new Size(
            finalSize.Width, 
            fullBottomLine < finalSize.Height 
                ? fullBottomLine
                : finalSize.Height);
    }

    private void ArrangeChilds(IReadOnlyList<ResponsiveGridRowContent> contents, double topLine, double lineHeight, double singleColumnWidth)
    {
        var currentColumnIndex = 0;
        foreach (var actContent in contents)
        {
            actContent.Control!.Arrange(new Rect(
                new Point(currentColumnIndex * singleColumnWidth, topLine),
                new Size(actContent.ColumnCount * singleColumnWidth, lineHeight)));
            currentColumnIndex += actContent.ColumnCount;
        }
    }

    private ResponsiveGridBreakpoint GetCurrentBreakpoint(double width)
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

    private int GetColumnCount(Control child, ResponsiveGridBreakpoint breakpoint)
    {
        var allBreakpoints = new ResponsiveGridBreakpoint[]
        {
            ResponsiveGridBreakpoint.Xs,
            ResponsiveGridBreakpoint.Sm,
            ResponsiveGridBreakpoint.Md,
            ResponsiveGridBreakpoint.Lg,
            ResponsiveGridBreakpoint.Xl,
            ResponsiveGridBreakpoint.Xxl
        };

        var columnCount = 0;
        for (var loop = 0; loop < allBreakpoints.Length; loop++)
        {
            var actColumnCount = GetColumnCountForBreakpoint(
                child, allBreakpoints[loop]);
            if (actColumnCount > 0)
            {
                columnCount = actColumnCount;
            }

            if (allBreakpoints[loop] == breakpoint)
            {
                break;
            }
        }

        if (columnCount == 0)
        {
            // TODO: Default column sizing (use available space)
            columnCount = 1;
        }
        
        return columnCount;
    }
    
    private int GetColumnCountForBreakpoint(Control child, ResponsiveGridBreakpoint breakpoint)
    {
        return breakpoint switch
        {
            ResponsiveGridBreakpoint.Xxl => child.GetValue(ColumnsXxlProperty),
            ResponsiveGridBreakpoint.Xl => child.GetValue(ColumnsXlProperty),
            ResponsiveGridBreakpoint.Lg => child.GetValue(ColumnsLgProperty),
            ResponsiveGridBreakpoint.Md => child.GetValue(ColumnsMdProperty),
            ResponsiveGridBreakpoint.Sm => child.GetValue(ColumnsSmProperty),
            _ => child.GetValue(ColumnsProperty),
        };
    }
}