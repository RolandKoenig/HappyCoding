using System;
using Avalonia;
using Avalonia.Controls;

namespace HappyCoding.AvaloniaStructuredList.Controls;

public class StructuredList : Panel
{
    public static readonly StyledProperty<double> IndentWidthProperty = 
        AvaloniaProperty.Register<StructuredList, double>(nameof(IndentWidth), defaultValue: 15d);
    public double IndentWidth
    {
        get => GetValue(IndentWidthProperty);
        set => SetValue(IndentWidthProperty, value);
    }
    
    public static readonly StyledProperty<double> SpacingProperty = 
        AvaloniaProperty.Register<StructuredList, double>(nameof(Spacing), defaultValue: 0d);
    public double Spacing
    {
        get => GetValue(SpacingProperty);
        set => SetValue(SpacingProperty, value);
    }
    
    /// <summary>
    /// Sets a fixed indent for an item in the <see cref="StructuredList"/>. The indent
    /// is applied for all following items too.
    /// </summary>
    public static readonly AttachedProperty<StructuredListFixedIndent> FixedIndentProperty =
        AvaloniaProperty.RegisterAttached<StructuredList, Control, StructuredListFixedIndent>(
            "FixedIndent",
            defaultValue: StructuredListFixedIndent.None,
            validate: _ => true);
    public static StructuredListFixedIndent GetFixedIndent(Control element)
        => element.GetValue(FixedIndentProperty);
    public static void SetFixedIndent(Control element, StructuredListFixedIndent value)
        => element.SetValue(FixedIndentProperty, value);
    
    /// <summary>
    /// Changes the indent which was set by the previous item.
    /// </summary>
    public static readonly AttachedProperty<StructuredListChangeIndentMode> ChangeIndentProperty =
        AvaloniaProperty.RegisterAttached<StructuredList, Control, StructuredListChangeIndentMode>(
            "ChangeIndent",
            defaultValue: StructuredListChangeIndentMode.None,
            validate: _ => true);
    public static StructuredListChangeIndentMode GetChangeIndent(Control element)
        => element.GetValue(ChangeIndentProperty);
    public static void SetChangeIndent(Control element, StructuredListChangeIndentMode value)
        => element.SetValue(ChangeIndentProperty, value);

    static StructuredList()
    {
        AffectsMeasure<StructuredList>(
            IndentWidthProperty,
            SpacingProperty);
    }
    
    protected override Size MeasureOverride(Size availableSize)
    {
        var width = 0.0;
        var height = 0.0;

        var indentWidth = this.IndentWidth;
        var currentIndent = 0;
        var displayedChildCount = 0;
        foreach (var child in Children)
        {
            // Respect indentations also if a child is not visible
            currentIndent = CalculateNextIndent(child, currentIndent);

            if (!child.IsVisible) { continue; }
            displayedChildCount++;
            
            var childHoldsIndentArea = child is StructuredListItem;
            var freeSpaceForIndents = childHoldsIndentArea
                ? currentIndent * indentWidth
                : (currentIndent + 1) * indentWidth;
            var childAvailableSize = new Size(
                freeSpaceForIndents >= availableSize.Width
                    ? 0f 
                    : availableSize.Width - freeSpaceForIndents,
                double.PositiveInfinity);
        
            child.Measure(childAvailableSize);

            width = Math.Max(width, freeSpaceForIndents + child.DesiredSize.Width);
            height += child.DesiredSize.Height;
        }

        if (displayedChildCount > 0)
        {
            height += (displayedChildCount - 1) * Spacing;
        }
        
        return new Size(width, height);
    }

    protected override Size ArrangeOverride(Size finalSize)
    {
        var indentWidth = this.IndentWidth;
        
        var currentY = 0.0;
        var currentIndent = 0;
        foreach (var child in Children)
        {
            // Respect indentations also if a child is not visible
            currentIndent = CalculateNextIndent(child, currentIndent);
            
            if (!child.IsVisible) { continue; }
            
            var structuredListItem = child as StructuredListItem;
            var childHoldsIndentArea = structuredListItem != null;
            var freeSpaceForIndents = childHoldsIndentArea
                ? currentIndent * indentWidth
                : (currentIndent + 1) * indentWidth;
            var availableWidth = freeSpaceForIndents > finalSize.Width 
                ? 0f 
                : finalSize.Width - freeSpaceForIndents;

            var childDesiredSize = child.DesiredSize;
            var childNewWidth = availableWidth < childDesiredSize.Width
                ? availableWidth
                : childDesiredSize.Width;

            if (currentY > 0)
            {
                currentY += this.Spacing;
            }
            
            child.Arrange(new Rect(
                freeSpaceForIndents, currentY, 
                childNewWidth, childDesiredSize.Height));
            currentY += childDesiredSize.Height;

            if (structuredListItem != null)
            {
                structuredListItem.IndentWidth = indentWidth;
                structuredListItem.ActualIndent = currentIndent;
            }
        }

        return finalSize;
    }

    private static int CalculateNextIndent(Control child, int currentIndent)
    {
        var nextIndent = currentIndent;
        
        // Apply indent changing
        var changeIndent = GetChangeIndent(child);
        if (changeIndent != StructuredListChangeIndentMode.None)
        {
            nextIndent = changeIndent switch
            {
                StructuredListChangeIndentMode.Increase => nextIndent + 1,
                StructuredListChangeIndentMode.Decrease => nextIndent - 1,
                _ => nextIndent
            };
        }

        // Apply fixed indent
        var fixedIndent = GetFixedIndent(child);
        if (fixedIndent != StructuredListFixedIndent.None)
        {
            nextIndent = (int)fixedIndent;
        }
        
        return nextIndent;
    }
}