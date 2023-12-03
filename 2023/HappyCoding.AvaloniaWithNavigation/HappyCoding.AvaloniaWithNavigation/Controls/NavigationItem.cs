using System;

namespace HappyCoding.AvaloniaWithNavigation.Controls;

public class NavigationItem
{
    public string Name { get; set; } = string.Empty;

    public Type? ViewType { get; set; } = null;
}
