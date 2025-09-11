namespace HappyCoding.AvaloniaCustomResponsiveControls.Controls;

class ResponsiveGridRow(IReadOnlyList<ResponsiveGridRowChild> children)
{
    public IReadOnlyList<ResponsiveGridRowChild> Children { get; } 
        = children;

    public int TotalColumns { get; } 
        = children.Sum(x => x.ColumnCount);
}