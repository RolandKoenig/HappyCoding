using System;
using System.Globalization;
using Avalonia;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace HappyCoding.AvaloniaReferencingValueConverters.ByStaticVariable;

public class VersionStringToBrushConverter : IValueConverter
{
    public static readonly VersionStringToBrushConverter Instance = new VersionStringToBrushConverter();
    
    private static readonly Version REFERENCE_VERSION = new Version(1, 0, 0, 0);
    
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    { 
        if (value is not string valueString)
        {
            return AvaloniaProperty.UnsetValue;
        }

        if (!Version.TryParse(valueString, out var parsedVersion))
        {
            return AvaloniaProperty.UnsetValue;
        }

        if (parsedVersion < REFERENCE_VERSION) { return Brushes.Red; }
        if (parsedVersion == REFERENCE_VERSION) { return Brushes.Yellow; }
        return Brushes.Green;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return BindingOperations.DoNothing;
    }
}