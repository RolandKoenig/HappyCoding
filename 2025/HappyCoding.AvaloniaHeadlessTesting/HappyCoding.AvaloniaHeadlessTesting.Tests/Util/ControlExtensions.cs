using Avalonia;
using Avalonia.Controls;
using Avalonia.LogicalTree;

namespace HappyCoding.AvaloniaHeadlessTesting.Tests.Util;

public static class ControlExtensions
{
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
    
    public static IEnumerable<Control> QueryLogicalChildrenDeep(this ILogical logicalTreeNode)
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