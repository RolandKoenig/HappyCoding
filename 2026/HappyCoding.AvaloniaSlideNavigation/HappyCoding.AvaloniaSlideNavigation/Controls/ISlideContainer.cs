namespace HappyCoding.AvaloniaSlideNavigation.Controls;

public interface ISlideContainer
{
    int GetSlideCount();

    Slide GetSlide(int slideIndex);
}