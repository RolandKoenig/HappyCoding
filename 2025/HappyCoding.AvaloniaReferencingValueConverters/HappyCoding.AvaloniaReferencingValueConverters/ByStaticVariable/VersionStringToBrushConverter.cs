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
    
    private static readonly Version VERSION_1_0 = new Version(1, 0, 0, 0);
    
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

        if (parsedVersion < VERSION_1_0) { return Brushes.Red; }
        if (parsedVersion == VERSION_1_0) { return Brushes.Yellow; }
        return Brushes.Green;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return BindingOperations.DoNothing;
    }
}