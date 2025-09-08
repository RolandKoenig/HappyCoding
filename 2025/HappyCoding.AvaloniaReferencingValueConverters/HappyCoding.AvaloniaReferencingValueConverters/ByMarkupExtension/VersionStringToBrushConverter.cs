using System;
using System.Globalization;
using Avalonia;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Markup.Xaml;
using Avalonia.Media;

namespace HappyCoding.AvaloniaReferencingValueConverters.ByMarkupExtension;

public class VersionStringToBrushConverter : MarkupExtension, IValueConverter
{
    public Version ReferenceVersion { get; set; } = new (1, 0, 0, 0);
    
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return this;
    }
    
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

        if (parsedVersion < ReferenceVersion) { return Brushes.Red; }
        if (parsedVersion == ReferenceVersion) { return Brushes.Yellow; }
        return Brushes.Green; 
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return BindingOperations.DoNothing;
    }
}