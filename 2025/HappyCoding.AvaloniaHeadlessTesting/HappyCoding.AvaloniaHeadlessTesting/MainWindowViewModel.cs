using System;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using HappyCoding.AvaloniaHeadlessTesting.Views;

namespace HappyCoding.AvaloniaHeadlessTesting;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty]
    private string _title = "HappyCoding AvaloniaHeadlessTesting";

    [ObservableProperty] 
    private IReadOnlyList<ViewInformation> _views;

    [ObservableProperty] 
    private ViewInformation? _selectedView;

    public MainWindowViewModel()
    {
        this.Views =
        [
            new ("Home", () => new HomeViewModel()),
            new ("Counter", () => new CounterViewModel()),
            new ("Weather", () => new WeatherDataViewModel())
        ];

        this.SelectedView = this.Views[0];
    }
    
    public class ViewInformation(string displayName, Func<object> viewModelFactory)
    {
        public string DisplayName => displayName;
        
        public object ViewModel => viewModelFactory();
    }
}