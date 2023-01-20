using System;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;

namespace HappyCoding.CommunityToolkitMvvm.DependencyInjection;

public class CreateUsingDependencyInjectionExtension : MarkupExtension
{
    public Type? Type { get; set; }

    public CreateUsingDependencyInjectionExtension()
    {
        
    }

    public CreateUsingDependencyInjectionExtension(Type type)
    {
        this.Type = type;
    }

    /// <inheritdoc />
    public override object? ProvideValue(IServiceProvider serviceProvider)
    {
        if (this.Type == null) { return null; }
        if (Design.IsDesignMode) { return null; }

        var rootObjProvider = serviceProvider.GetService<IRootObjectProvider>();

        if (rootObjProvider?.RootObject is not IControl rootObj) { return null; }
        
        var appServiceProvider = 
            rootObj.FindResource("AppServiceProvider") as IServiceProvider;

        return appServiceProvider?.GetRequiredService(this.Type);
    }
}