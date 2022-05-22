using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;

namespace HappyCoding.SimpleWinUI3App.Util.Converter;

internal class BoolToVisibilityConverter : IValueConverter
{
    public Visibility VisibilityOnTrue
    {
        get;
        set;
    } = Visibility.Visible;

    public Visibility VisibilityOnFalse
    {
        get;
        set;
    } = Visibility.Collapsed;

    public Visibility VisibilityOnNull
    {
        get;
        set;
    } = Visibility.Collapsed;

    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is not bool valueBool)
        {
            return this.VisibilityOnNull;
        }

        return valueBool ? this.VisibilityOnTrue : this.VisibilityOnFalse;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
