using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace HappyCoding.ConsoleLogWindow.Gui.Converter;

public class DateTimeOffsetFormattingConverter : IValueConverter
{
    /// <inheritdoc />
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not DateTimeOffset dateTimeOffset)
        {
            return "";
        }
        return dateTimeOffset.ToString("yyyy-MM-dd HH:mm:ss.fff");
    }

    /// <inheritdoc />
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
