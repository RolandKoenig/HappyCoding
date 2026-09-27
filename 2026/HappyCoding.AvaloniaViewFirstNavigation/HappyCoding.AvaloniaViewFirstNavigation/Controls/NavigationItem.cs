using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;

namespace HappyCoding.AvaloniaViewFirstNavigation.Controls;

public class NavigationItem : AvaloniaObject
{
    /// <summary>
    /// Defines the <see cref="Header"/> property.
    /// </summary>
    public static readonly StyledProperty<object?> HeaderProperty =
        AvaloniaProperty.Register<NavigationItem, object?>(nameof(Header));
    
    /// <summary>
    /// Defines the <see cref="Source"/> property.
    /// </summary>
    public static readonly StyledProperty<Uri?> SourceProperty =
        AvaloniaProperty.Register<NavigationItem, Uri?>(nameof(Source));
    
    private readonly IServiceProvider _serviceProvider;
    private readonly Uri? _baseUri;
    
    /// <summary>
    /// Gets or sets the header content.
    /// </summary>
    public object? Header
    {
        get => GetValue(HeaderProperty);
        set => SetValue(HeaderProperty, value);
    }
    
    /// <summary>
    /// Gets or sets the source URL.
    /// </summary>
    public Uri? Source
    {
        get => GetValue(SourceProperty);
        set => SetValue(SourceProperty, value);
    }
    
    public NavigationItem(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _baseUri = serviceProvider.GetRequiredService<IUriContext>().BaseUri;
    }

    internal Control? TryCreateControl()
    {
        if (this.Source == null) { return null; }

        return AvaloniaXamlLoader.Load(_serviceProvider, this.Source, _baseUri) as Control;
    }
}