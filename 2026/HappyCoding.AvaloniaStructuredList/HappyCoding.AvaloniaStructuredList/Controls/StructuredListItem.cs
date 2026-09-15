using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using Avalonia.Metadata;

namespace HappyCoding.AvaloniaStructuredList.Controls;

[PseudoClasses(
    ":indent-zero",
    ":indent-one",
    ":indent-two",
    ":indent-three",
    ":indent-four",
    ":indent-five",
    ":indent-six")]
public class StructuredListItem : TemplatedControl
{
    public static readonly StyledProperty<object?> ContentProperty = 
        AvaloniaProperty.Register<StructuredListItem, object?>(nameof(Content));
    [Content]
    public object? Content
    {
        get => GetValue(ContentProperty);
        set => SetValue(ContentProperty, value);
    }
    
    public static readonly StyledProperty<object?> BulletContentProperty = 
        AvaloniaProperty.Register<StructuredListItem, object?>(nameof(BulletContent), defaultValue: "•");
    public object? BulletContent
    {
        get => GetValue(BulletContentProperty);
        set => SetValue(BulletContentProperty, value);
    }
    
    public static readonly StyledProperty<IBrush?> BulletForegroundProperty = 
        AvaloniaProperty.Register<StructuredListItem, IBrush?>(nameof(BulletForeground), defaultValue: null);
    public IBrush? BulletForeground
    {
        get => GetValue(BulletForegroundProperty);
        set => SetValue(BulletForegroundProperty, value);
    }
    
    public static readonly StyledProperty<double> IndentWidthProperty = 
        AvaloniaProperty.Register<StructuredListItem, double>(nameof(IndentWidth));
    public double IndentWidth
    {
        get => GetValue(IndentWidthProperty);
        set => SetValue(IndentWidthProperty, value);
    }
    
    public static readonly StyledProperty<int> ActualIndentProperty = 
        AvaloniaProperty.Register<StructuredListItem, int>(nameof(ActualIndent));
    public int ActualIndent
    {
        get => GetValue(ActualIndentProperty);
        set => SetValue(ActualIndentProperty, value);
    }
    
    public static readonly StyledProperty<TextWrapping> TextWrappingProperty = 
        TextBlock.TextWrappingProperty.AddOwner<StructuredListItem>();
    public TextWrapping TextWrapping
    {
        get => this.GetValue(TextWrappingProperty);
        set => this.SetValue(TextWrappingProperty, value);
    }
    
    public StructuredListItem()
    {
        this.UpdatePseudoClasses();
    }
    
    private void UpdatePseudoClasses()
    {
        var actualIndent = ActualIndent;
        this.PseudoClasses.Set(":indent-zero", actualIndent == 0);
        this.PseudoClasses.Set(":indent-one", actualIndent == 1);
        this.PseudoClasses.Set(":indent-two", actualIndent == 2);
        this.PseudoClasses.Set(":indent-three", actualIndent == 3);
        this.PseudoClasses.Set(":indent-four", actualIndent == 4);
        this.PseudoClasses.Set(":indent-five", actualIndent == 5);
        this.PseudoClasses.Set(":indent-six", actualIndent == 6);
    }
    
    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);

        if (change.Property == ActualIndentProperty)
        {
            UpdatePseudoClasses();
        }
    }
}