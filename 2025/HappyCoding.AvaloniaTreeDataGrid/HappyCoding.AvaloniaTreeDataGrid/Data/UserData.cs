using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace HappyCoding.AvaloniaTreeDataGrid.Data;

public partial class UserData : ObservableObject
{
    [ObservableProperty]
    private string _gender = string.Empty;
    
    [ObservableProperty]
    private string _firstName = string.Empty;

    [ObservableProperty]
    private string _lastName = string.Empty;

    [ObservableProperty]
    private string _userName = string.Empty;

    [ObservableProperty] 
    private string _eMail = string.Empty;
    
    public IReadOnlyList<UserData> Children { get; set; } = new ObservableCollection<UserData>();
}