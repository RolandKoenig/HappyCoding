using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Presenters;
using Avalonia.Controls.Primitives;
using Avalonia.Metadata;

namespace HappyCoding.AvaloniaViewFirstNavigation.Controls;

[TemplatePart("PART_PagePresenter", typeof(ContentPresenter))]
public class NavigationControl : TemplatedControl
{
    public static readonly DirectProperty<NavigationControl, ObservableCollection<NavigationItem>> ItemsProperty =
        AvaloniaProperty.RegisterDirect<NavigationControl, ObservableCollection<NavigationItem>>(
            nameof(Items),
            (x) => x.Items);
    
    public static readonly StyledProperty<NavigationItem?> SelectedItemProperty =
        AvaloniaProperty.Register<NavigationControl, NavigationItem?>(nameof(SelectedItem));
    
    public NavigationItem? SelectedItem
    {
        get => GetValue(SelectedItemProperty);
        set => SetValue(SelectedItemProperty, value);
    }

    [Content]
    public ObservableCollection<NavigationItem> Items { get; } = new ();

    private ContentPresenter? _pagePresenter;
    
    public NavigationControl()
    {
        Items.CollectionChanged += OnItemsCollectionChanged;
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        
        _pagePresenter = e.NameScope.Find<ContentPresenter>("PART_PagePresenter");
    }

    private void OnItemsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (this.SelectedItem == null) { return; }
        
        if ((e.OldItems?.Contains(this.SelectedItem) == true) || 
            (e.Action == NotifyCollectionChangedAction.Reset))
        {
            _pagePresenter?.Content = null;
        }
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);

        if (change.Property == SelectedItemProperty)
        {
            var selectedItem = GetValue(SelectedItemProperty);
            _pagePresenter?.Content = selectedItem?.TryCreateControl();
        }
    }
}