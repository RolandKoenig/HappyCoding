using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Metadata;

namespace HappyCoding.AvaloniaSlideNavigation.Controls;

public class SlideDeck : ContentControl, ISlideContainer
{
    [Content]
    public List<ISlideContainer> Slides
    {
        get;
    } = new ();

    public int GetSlideCount()
    {
        return this.Slides
            .Sum(x => x.GetSlideCount());
    }

    public Slide GetSlide(int slideIndex)
    {
        var currentStartIndex = 0;
        foreach (var actSlideContainer in this.Slides)
        {
            var actSlideCount = actSlideContainer.GetSlideCount();
            
            if (slideIndex < currentStartIndex + actSlideCount)
            {
                return actSlideContainer.GetSlide(slideIndex - currentStartIndex);
            }

            currentStartIndex += actSlideCount;
        }

        throw new IndexOutOfRangeException($"Slide with index {slideIndex} does not exist");
    }
}