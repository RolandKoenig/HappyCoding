using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;

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
            "ColumnsSm",
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

    public static readonly StyledProperty<HorizontalAlignment> RowAlignmentProperty =
        AvaloniaProperty.Register<ResponsiveGrid, HorizontalAlignment>(
            nameof(RowAlignment), 
            defaultValue: HorizontalAlignment.Left);
    
    public static readonly StyledProperty<double> RowSpacingProperty =
        AvaloniaProperty.Register<ResponsiveGrid, double>(
            nameof(RowSpacing), 
            defaultValue: 0d);
    
    private ResponsiveGridBreakpoint _currentBreakpoint = ResponsiveGridBreakpoint.Sm;
    private IReadOnlyList<ResponsiveGridRow> _currentRows = [];
    
    /// <summary>
    /// Gets the breakpoint calculated in the last measure pass.
    /// </summary>
    public ResponsiveGridBreakpoint CurrentBreakpoint => _currentBreakpoint;

    public HorizontalAlignment RowAlignment
    {
        get => GetValue(RowAlignmentProperty);
        set => SetValue(RowAlignmentProperty, value);
    }
    
    public double RowSpacing
    {
        get => GetValue(RowSpacingProperty);
        set => SetValue(RowSpacingProperty, value);
    }
    
    static ResponsiveGrid()
    {
        AffectsMeasure<ResponsiveGrid>(
            RowAlignmentProperty,
            RowSpacingProperty);
        AffectsParentMeasure<ResponsiveGrid>(
            ColumnsProperty, ColumnsSmProperty, ColumnsMdProperty,
            ColumnsLgProperty, ColumnsXlProperty, ColumnsXxlProperty);
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
        _currentBreakpoint = ResponsiveBreakpointUtil.GetCurrentBreakpoint(availableSize.Width);
        
        var singleColumnWidth = double.IsFinite(availableSize.Width)
            ? availableSize.Width / 12.0
            : double.PositiveInfinity;
        
        var fullBottomLine = 0d;
        var fullDesiredWith = 0d;
        _currentRows = this.CalculateRows();
        foreach (var actRow in _currentRows)
        {
            var actRowBottomLine = 0d;
            var actRowDesiredWith = 0d;
            foreach (var actChild in actRow.Children)
            {
                var actChildControl = actChild.ChildControl!;
                actChildControl.Measure(new Size(
                    singleColumnWidth * actChild.ColumnCount, 
                    availableSize.Height));
                
                var actChildDesiredSize = actChildControl.DesiredSize;
                if (actChildDesiredSize.Height > actRowBottomLine)
                {
                    actRowBottomLine = actChildDesiredSize.Height;
                }
                actRowDesiredWith += actChildDesiredSize.Width;
            }

            fullBottomLine += actRowBottomLine;
            if (actRowDesiredWith > fullDesiredWith)
            {
                fullDesiredWith = actRowDesiredWith;
            }
        }
        
        return new Size(
            double.IsFinite(availableSize.Width)
                ? availableSize.Width
                : fullDesiredWith, 
            fullBottomLine);
    }

    protected override Size ArrangeOverride(Size finalSize)
    {
        var singleColumnWidth = finalSize.Width / 12.0;
        
        var fullBottomYPosition = 0d;
        for (var loop = 0; loop < _currentRows.Count; loop++)
        {
            var actChild = _currentRows[loop];
            var actRowHeight = actChild.Children.Max(x => x.ChildControl!.DesiredSize.Height);

            if (loop > 0)
            {
                fullBottomYPosition += this.RowSpacing;
            }
            
            ArrangeChilds(
                actChild.Children,
                fullBottomYPosition,
                actRowHeight,
                singleColumnWidth,
                finalSize.Width);

            fullBottomYPosition += actRowHeight;
        }

        if (_currentRows.Count > 1)
        {
            fullBottomYPosition += (_currentRows.Count - 1) * this.RowSpacing;
        }
        
        return new Size(
            finalSize.Width, fullBottomYPosition);
    }
    
    private void ArrangeChilds(
        IReadOnlyList<ResponsiveGridRowChild> contents, 
        double startYPosition, double rowHeight, double singleColumnWidth, double availableWidth)
    {
        var startXPosition = 0d;
        var extendedWidthPerColumn = 0d;
        if (RowAlignment != HorizontalAlignment.Left)
        {
            var occupiedColumns = contents.Sum(x => x.ColumnCount);
            var remainingSpace = (12 - occupiedColumns) * singleColumnWidth;
            if (remainingSpace > 0d)
            {
                startXPosition = RowAlignment switch
                {
                    HorizontalAlignment.Center => remainingSpace / 2d,
                    HorizontalAlignment.Right => remainingSpace,
                    _ => 0d
                };
                extendedWidthPerColumn = RowAlignment switch
                {
                    HorizontalAlignment.Stretch => remainingSpace / (double)occupiedColumns,
                    _ => 0d
                };
            }
        }
        
        var currentColumnIndex = 0;
        foreach (var actContent in contents)
        {
            var controlXPosition =
                startXPosition +
                currentColumnIndex * (singleColumnWidth + extendedWidthPerColumn);
            var controlWidth = actContent.ColumnCount * (singleColumnWidth + extendedWidthPerColumn);
            actContent.ChildControl!.Arrange(new Rect(
                new Point(controlXPosition, startYPosition),
                new Size(controlWidth, rowHeight)));
            currentColumnIndex += actContent.ColumnCount;
        }
    }

    private IReadOnlyList<ResponsiveGridRow> CalculateRows()
    {
        var result = new List<ResponsiveGridRow>(4);
        
        var actRow = new List<ResponsiveGridRowChild>(12);
        var actColumnCount = 0;
        var breakpoint = this.CurrentBreakpoint;
        foreach (var actChild in this.Children)
        {
            var actChildColumnCount = GetColumnCount(actChild, breakpoint);
            var columnCountForCalculation = actChildColumnCount > 0
                ? actChildColumnCount
                : 1;
            if (actColumnCount + columnCountForCalculation > 12)
            {
                // Transit to new row
                result.Add(new ResponsiveGridRow(actRow.ToArray()));
                actRow.Clear();
                actColumnCount = 0;
            }

            actColumnCount += columnCountForCalculation;
            actRow.Add(new ResponsiveGridRowChild(
                actChild, actChildColumnCount));
        }

        // Finish last row
        if (actRow.Count > 0)
        {
            result.Add(new ResponsiveGridRow(actRow.ToArray()));
        }

        return result;
    }
    
    private int GetColumnCount(Control child, ResponsiveGridBreakpoint breakpoint)
    {
        var allBreakpoints = new[]
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
        
        return columnCount;
    }
    
    private int GetColumnCountForBreakpoint(Control child, ResponsiveGridBreakpoint breakpoint)
    {
        var result = breakpoint switch
        {
            ResponsiveGridBreakpoint.Xxl => child.GetValue(ColumnsXxlProperty),
            ResponsiveGridBreakpoint.Xl => child.GetValue(ColumnsXlProperty),
            ResponsiveGridBreakpoint.Lg => child.GetValue(ColumnsLgProperty),
            ResponsiveGridBreakpoint.Md => child.GetValue(ColumnsMdProperty),
            ResponsiveGridBreakpoint.Sm => child.GetValue(ColumnsSmProperty),
            _ => child.GetValue(ColumnsProperty),
        };
        
        if (result < 0) { return 0; }
        if (result > 12) { return 12; }
        return result;
    }
}