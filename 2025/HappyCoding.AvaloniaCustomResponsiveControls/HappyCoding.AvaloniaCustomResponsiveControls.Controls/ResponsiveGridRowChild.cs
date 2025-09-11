using Avalonia.Controls;

namespace HappyCoding.AvaloniaCustomResponsiveControls.Controls;

struct ResponsiveGridRowChild(Control childControl, int columnCount)
{
    public readonly Control? ChildControl = childControl;
    public readonly int ColumnCount = columnCount;
}