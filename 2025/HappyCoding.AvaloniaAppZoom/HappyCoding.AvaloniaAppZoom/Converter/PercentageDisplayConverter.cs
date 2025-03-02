using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace HappyCoding.AvaloniaAppZoom.Converter;

public class PercentageDisplayConverter : IValueConverter
{
    public static readonly PercentageDisplayConverter Instance = new();
    
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not int percentage)
        {
            return null;
        }

        return $"{percentage} %";
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        // Not needed
        
        return null;
    }
}