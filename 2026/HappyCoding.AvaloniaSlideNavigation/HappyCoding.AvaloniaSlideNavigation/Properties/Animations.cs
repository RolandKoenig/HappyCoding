using System.Collections.Generic;
using Avalonia;
using Avalonia.Data;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;

namespace HappyCoding.AvaloniaSlideNavigation.Properties;

// ReSharper disable once ClassNeverInstantiated.Global
public class Animations : AvaloniaObject
{
    public static readonly AttachedProperty<int> OnClickProperty =
        AvaloniaProperty.RegisterAttached<Animations, Interactive, int>(
            "OnClick", 0, false, BindingMode.OneTime);
    
    public static void SetOnClick(AvaloniaObject element, int value) 
        => element.SetValue(OnClickProperty, value);
    
    public static int GetOnClick(AvaloniaObject element) 
        => element.GetValue(OnClickProperty);

    public static int GetMaximumClicks(IReadOnlyList<ILogical> logicals)
    {
        var actMaximum = 0;
        foreach (var actElement in logicals)
        {
            if (actElement is Interactive actInteractive)
            {
                var actOnClick = Animations.GetOnClick(actInteractive);
                if (actOnClick > actMaximum) { actMaximum = actOnClick; }
            }

            var actMaximumDeep = GetMaximumClicks(actElement.LogicalChildren);
            if (actMaximumDeep > actMaximum)
            {
                actMaximum = actMaximumDeep;
            }
        }
        return actMaximum;
    }
    
    public static void SetCurrentClick(IReadOnlyList<ILogical> logicals, int click)
    {
        foreach (var actElement in logicals)
        {
            if (actElement is Interactive actInteractive)
            {
                var actOnClick = Animations.GetOnClick(actInteractive);
                actInteractive.IsVisible = actOnClick <= click;
            }
            
            SetCurrentClick(actElement.LogicalChildren, click);
        }
    }
}