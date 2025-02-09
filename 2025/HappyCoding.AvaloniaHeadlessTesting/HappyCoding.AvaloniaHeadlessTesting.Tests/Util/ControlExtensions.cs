using Avalonia;
using Avalonia.Controls;
using Avalonia.LogicalTree;
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
    
    public static T LocateLogicalChildOfTypeWithName<T>(this ILogical logicalTreeNode, string name)
        where T : StyledElement
    {
        var result = logicalTreeNode.QueryLogicalChildrenDeep()
            .OfType<T>()
            .FirstOrDefault(x => x.Name == name);
        if (result is null)
        {
            throw new LocateElementException(
                $"Unable to locate logical child of type '{typeof(T).FullName}' with name '{name}'!");
        }
        return result;
    }
    
    public static T LocateLogicalChildOfTypeWithClass<T>(this ILogical logicalTreeNode, string className)
        where T : StyledElement
    {
        var result = logicalTreeNode.QueryLogicalChildrenDeep()
            .OfType<T>()
            .FirstOrDefault(x => x.Classes.Contains(className));
        if (result is null)
        {
            throw new LocateElementException(
                $"Unable to locate logical child of type '{typeof(T).FullName}' with class '{className}'!");
        }
        return result;
    }
    
    public static T LocateLogicalChildOfType<T>(this ILogical logicalTreeNode)
    {
        var result = logicalTreeNode.QueryLogicalChildrenDeep()
            .OfType<T>()
            .FirstOrDefault();
        if (result is null)
        {
            throw new LocateElementException(
                $"Unable to locate logical child of type '{typeof(T).FullName}'!");
        }
        return result;
    }

    public static TextBlock LocateLogicalChildTextBlockWithText(this ILogical logicalTreeNode, string text)
    {
        var result = logicalTreeNode.QueryLogicalChildrenDeep()
            .OfType<TextBlock>()
            .FirstOrDefault(x => x.Text == text);
        if (result is null)
        {
            throw new LocateElementException(
                $"Unable to locate logical child TextBlock with text '{text}'!");
        }
        return result;
    }
    
    public static Visual LocateByText(this ILogical logicalTreeNode, string text)
    {
        var result = logicalTreeNode.QueryLogicalChildrenDeep()
            .OfType<Visual>()
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
    
    public static Visual LocateByClass(this ILogical logicalTreeNode, string className)
    {
        var result = logicalTreeNode.QueryLogicalChildrenDeep()
            .OfType<Visual>()
            .FirstOrDefault(x => x.Classes.Contains(className));
        if (result is null)
        {
            throw new LocateElementException(
                $"Unable to locate logical child with class '{className}'!");
        }
        return result;
    }

    public static Visual LocateBySelection(this ILogical logicalTreeNode)
    {
        var result = logicalTreeNode.QueryLogicalChildrenDeep()
            .OfType<Visual>()
            .FirstOrDefault(x =>
                (x is ListBoxItem { IsSelected: true }) ||
                (x is MenuItem { IsSelected: true }) ||
                (x is TabItem { IsSelected: true }) ||
                (x is ComboBoxItem { IsSelected: true }) ||
                (x is TreeViewItem { IsSelected: true }));
                // There may be more...
        if (result is null)
        {
            throw new LocateElementException(
                "Unable to locate logical child which is selected!");
        }
        return result;
    }
    
    public static Visual LocateByName(this ILogical logicalTreeNode, string name)
    {
        var result = logicalTreeNode.QueryLogicalChildrenDeep()
            .OfType<Visual>()
            .FirstOrDefault(x => x.Name == name);
        if (result is null)
        {
            throw new LocateElementException(
                $"Unable to locate logical child with name '{name}'!");
        }
        return result;
    }
    
    public static Visual LocateByTestId(this ILogical logicalTreeNode, string testId)
    {
        var result = logicalTreeNode.QueryLogicalChildrenDeep()
            .OfType<Visual>()
            .FirstOrDefault(x => x.GetValue(TestProperties.TestIdProperty) == testId);
        if (result is null)
        {
            throw new LocateElementException(
                $"Unable to locate logical child with TestId '{testId}'!");
        }
        return result;
    }

    public static ListBoxItem LocateSelectedItem(this ListBox listBox)
    {
        var result = listBox.QueryLogicalChildrenDeep()
            .OfType<ListBoxItem>()
            .FirstOrDefault(x => x.IsSelected);
        if (result is null)
        {
            throw new LocateElementException(
                "Unable to locate selected ListBoxItem!");
        }
        return result;
    }

    private static IEnumerable<Control> QueryLogicalChildrenDeep(this ILogical logicalTreeNode)
    {
        foreach (var actChild in logicalTreeNode.GetLogicalChildren())
        {
            if (actChild is Control actChildControl)
            {
                yield return actChildControl;
            }

            foreach (var actInnerChildControl in actChild.QueryLogicalChildrenDeep())
            {
                yield return actInnerChildControl;
            }
        }
    }
}