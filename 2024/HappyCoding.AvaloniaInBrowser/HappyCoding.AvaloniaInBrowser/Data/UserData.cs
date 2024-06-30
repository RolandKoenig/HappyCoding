using System.Diagnostics.CodeAnalysis;
using CommunityToolkit.Mvvm.ComponentModel;

namespace HappyCoding.AvaloniaInBrowser.Data;

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
}