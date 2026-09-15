using System;
using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Markup.Xaml;

namespace HappyCoding.AvaloniaStructuredList.Controls;

internal class StructuredListItemOpacityCalculator : MarkupExtension, IValueConverter
{
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return this;
    }

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value == null) { return 0d; }

        if (value is string stringValue &&
            string.IsNullOrEmpty(stringValue))
        {
            return 0d;
        }
        
        return 1d;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return BindingOperations.DoNothing;
    }
}