using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using CommunityToolkit.Mvvm.ComponentModel;

namespace HappyCoding.AvaloniaHeadlessTesting;

public class ViewLocator : IDataTemplate
{
    public bool SupportsRecycling => false;

    public Control Build(object? data)
    {
        if (data == null) { return BuildDefaultControl(); }

        var dataTypeFullName = data.GetType().FullName;
        if(string.IsNullOrEmpty(dataTypeFullName)) { return BuildDefaultControl(); }
        
        var expectedViewTypeName = dataTypeFullName.Replace("ViewModel", "View");
        var viewType = data.GetType().Assembly.GetType(expectedViewTypeName);
        if (viewType == null) { return BuildDefaultControl(); }

        return (Control)Activator.CreateInstance(viewType)!;
    }

    public bool Match(object? data)
    {
        return data is ObservableObject;
    }
    
    private Control BuildDefaultControl()
    {
        return new TextBlock
        {
            Text = "Not Found"
        };
    }
}