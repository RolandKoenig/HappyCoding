using Avalonia.Controls;

namespace HappyCoding.AvaloniaCustomResponsiveControls.Controls;

struct ResponsiveGridRowContent(Control control, int columnCount)
{
    public readonly Control? Control = control;
    public readonly int ColumnCount = columnCount;
}