using Avalonia;
using Avalonia.Controls;
using Avalonia.VisualTree;
using HappyCoding.AvaloniaHeadlessTesting.Toolkit;

namespace HappyCoding.AvaloniaHeadlessTesting.Tests.Util;

public static class ControlExtensions
{
    public static string? ReadTextFromVisual(this Visual visual)
    {
        if (visual is TextBlock textBlock) { return textBlock.Text; }
        else if (visual is TextBox textBox) { return textBox.Text; }
        else
        {
            throw new ArgumentException($"Unable to get text from {visual.GetType().FullName}");
        }
    }
    
    public static T LocateByType<T>(this Visual visualTreeNode)
    {
        var result = visualTreeNode.QueryVisualChildrenDeep()
            .OfType<T>()
            .FirstOrDefault();
        if (result is null)
        {
            throw new LocateElementException(
                $"Unable to locate logical child of type '{typeof(T).FullName}'!");
        }
        return result;
    }
    
    public static Visual LocateByText(this Visual visualTreeNode, string text)
    {
        var result = visualTreeNode.QueryVisualChildrenDeep()
            .FirstOrDefault(x =>
                (x is TextBlock textBlock && textBlock.Text == text) ||
                (x is TextBox textBox && textBox.Text == text));
        if (result is null)
        {
            throw new LocateElementException(
                $"Unable to locate logical child with text '{text}'!");
        }
        return result;
    }
    
    public static Visual LocateByClass(this Visual visualTreeNode, string className)
    {
        var result = visualTreeNode.QueryVisualChildrenDeep()
            .FirstOrDefault(x => x.Classes.Contains(className));
        if (result is null)
        {
            throw new LocateElementException(
                $"Unable to locate logical child with class '{className}'!");
        }
        return result;
    }

    public static Visual LocateBySelection(this Visual visualTreeNode)
    {
        var result = visualTreeNode.QueryVisualChildrenDeep()
            .FirstOrDefault(x =>
                (x is ListBoxItem { IsSelected: true }) ||
                (x is MenuItem { IsSelected: true }) ||
                (x is TabItem { IsSelected: true }) ||
                (x is ComboBoxItem { IsSelected: true }) ||
                (x is TreeViewItem { IsSelected: true }) ||
                (x is DataGridRow { IsSelected: true }));
                // There may be more...
        if (result is null)
        {
            throw new LocateElementException(
                "Unable to locate logical child which is selected!");
        }
        return result;
    }
    
    public static Visual LocateByTestId(this Visual visualTreeNode, string testId)
    {
        var result = visualTreeNode.QueryVisualChildrenDeep()
            .FirstOrDefault(x => x.GetValue(TestProperties.TestIdProperty) == testId);
        if (result is null)
        {
            throw new LocateElementException(
                $"Unable to locate logical child with TestId '{testId}'!");
        }
        return result;
    }
    
    private static IEnumerable<Visual> QueryVisualChildrenDeep(this Visual visualTreeNode)
    {
        foreach (var actChildVisual in visualTreeNode.GetVisualChildren())
        {
            yield return actChildVisual;
            foreach (var actInnerChildControl in actChildVisual.QueryVisualChildrenDeep())
            {
                yield return actInnerChildControl;
            }
        }
    }
}