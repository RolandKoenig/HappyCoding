using System;
using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Markup.Xaml;

namespace HappyCoding.AvaloniaImageViewer.Converters;

public class MultiplyDoubleConverter : MarkupExtension, IValueConverter
{
    public double Multiplier { get; set; } = 1;
    
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return this;
    }

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not double doubleValue)
        {
            return BindingOperations.DoNothing;
        }

        return doubleValue * this.Multiplier;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return BindingOperations.DoNothing;
    }
}