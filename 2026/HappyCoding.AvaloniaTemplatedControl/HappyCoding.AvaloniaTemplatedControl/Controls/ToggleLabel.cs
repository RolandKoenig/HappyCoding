using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;

namespace HappyCoding.AvaloniaTemplatedControl.Controls;

public class ToggleLabel : TemplatedControl
{
    public static readonly StyledProperty<string> LabelTextProperty =
        AvaloniaProperty.Register<ToggleLabel, string>(nameof(LabelText), "Default");

    public string LabelText
    {
        get => GetValue(LabelTextProperty);
        set => SetValue(LabelTextProperty, value);
    }
    
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        var button = e.NameScope.Find<Button>("PART_ToggleButton");
        if (button is not null)
        {
            button.Click += OnPartButton_Click;
        }
    }

    private void OnPartButton_Click(object? sender, RoutedEventArgs e)
    {
        Console.WriteLine("Toggle clicked");
    }
}