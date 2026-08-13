using Avalonia;
using Avalonia.Controls;
using HappyCoding.AvaloniaSlideNavigation.Properties;

namespace HappyCoding.AvaloniaSlideNavigation.Controls;

public class Slide : Panel, ISlideContainer
{
    public Slide()
    {
        this.Margin = new Thickness(30);
    }
    
    public void SetCurrentClick(int click)
    {
        var maxClicks = this.GetClickCount();
        if (click >= maxClicks)
        {
            click = maxClicks;
        }

        Animations.SetCurrentClick(this.LogicalChildren, click);
    }
    
    public int GetClickCount()
    {
        return Animations.GetMaximumClicks(this.LogicalChildren);
    }

    public int GetSlideCount()
    {
        return 1;
    }

    public Slide GetSlide(int slideIndex)
    {
        return this;
    }
}