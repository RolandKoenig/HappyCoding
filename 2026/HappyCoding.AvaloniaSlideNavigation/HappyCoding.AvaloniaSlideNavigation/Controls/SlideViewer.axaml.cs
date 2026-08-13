using Avalonia.Controls;
using Avalonia.Interactivity;

namespace HappyCoding.AvaloniaSlideNavigation.Controls;

public partial class SlideViewer : UserControl
{
    private int _currentSlideIndex;
    private int _currentClickIndex;
    
    public ISlideContainer? Slides { get; set; } = null;

    public Slide? CurrentSlide
    {
        get => this.CtrlSlideHost.Content as Slide;
        set => this.CtrlSlideHost.Content = value;
    }
    
    public SlideViewer()
    {
        InitializeComponent();
    }

    public void GoToFirstSlide()
    {
        if (this.Slides != null)
        {
            _currentSlideIndex = 0;
            _currentClickIndex = 0;
            
            var slide = this.Slides.GetSlide(_currentSlideIndex);
            slide.SetCurrentClick(_currentSlideIndex);
            
            this.CtrlSlideHost.Content = this.Slides.GetSlide(_currentSlideIndex);
        }
        else
        {
            this.CtrlSlideHost.Content = null;
        }
        
        this.UpdateState();
    }
    
    public void GoFoward()
    {
        if (this.Slides == null) { return; }
        if (this.Slides.GetSlideCount() -1 <= _currentSlideIndex) { return; }

        if (_currentClickIndex < this.CurrentSlide!.GetClickCount())
        {
            _currentClickIndex++;
            this.CurrentSlide!.SetCurrentClick(_currentClickIndex);
        }
        else
        {
            _currentSlideIndex++;
            _currentClickIndex = 0;
        
            var slide = this.Slides.GetSlide(_currentSlideIndex);
            slide.SetCurrentClick(_currentClickIndex);
            this.CurrentSlide = slide;
        }
        
        this.UpdateState();
    }

    public void GoBackward()
    {
        if (_currentSlideIndex <= 0 && _currentClickIndex <= 0) { return; }
        if (this.Slides == null) { return; }

        if (_currentClickIndex > 0)
        {
            _currentClickIndex--;
            this.CurrentSlide!.SetCurrentClick(_currentClickIndex);
        }
        else
        {
            _currentSlideIndex--;
            var slide = this.Slides.GetSlide(_currentSlideIndex);
            
            _currentClickIndex = slide.GetClickCount();
            slide.SetCurrentClick(_currentClickIndex);
            this.CurrentSlide = slide;
        }

        this.UpdateState();
    }

    private void UpdateState()
    {
        if (this.Slides == null)
        {
            this.CtrlSlideState.Header = string.Empty;
            return;
        }
        
        this.CtrlSlideState.Header = $"Slide {_currentSlideIndex} / {this.Slides.GetSlideCount()}, Click {_currentClickIndex}";
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);

        this.GoToFirstSlide();
    }

    private void OnMnuBack_Click(object? sender, RoutedEventArgs e)
    {
        this.GoBackward();
    }

    private void OnMnuForward_Click(object? sender, RoutedEventArgs e)
    {
        this.GoFoward();
    }

    private void OnMnuHome_Click(object? sender, RoutedEventArgs e)
    {
        this.GoToFirstSlide();
    }
}