using System;
using Microsoft.UI.Xaml.Markup;

namespace HappyCoding.SimpleWinUI3App.Pages;

internal class NavigationTargetExtension : MarkupExtension
{
    public NavigationTarget Target { get; set; }

    protected override object ProvideValue()
    {
        return Target switch
        {
            NavigationTarget.Home => typeof(Home).FullName!,
            NavigationTarget.InputForm => typeof(InputForm).FullName!,
            NavigationTarget.SearchGrid => typeof(SearchGrid).FullName!,
            NavigationTarget.WebView2Host => typeof(WebView2Host).FullName!,
            NavigationTarget.TabControlPage => typeof(TabControlPage).FullName!,
            _ => throw new InvalidOperationException($"Unknown member {Target}!")
        };
    }
}
