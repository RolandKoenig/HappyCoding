using System.Diagnostics;
using Avalonia.Controls;
using Avalonia.Controls.Models.TreeDataGrid;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HappyCoding.AvaloniaTreeDataGrid.Data;
using HappyCoding.AvaloniaTreeDataGrid.Services;

namespace HappyCoding.AvaloniaTreeDataGrid.Views;

public partial class FlatDataViewModel : ObservableObject
{
    [ObservableProperty]
    private FlatTreeDataGridSource<UserData> _dataSource;
    
    public FlatDataViewModel(ITestDataGenerator testDataGenerator)
    {
        this.DataSource = new FlatTreeDataGridSource<UserData>(testDataGenerator.GenerateUserData(50));
        this.DataSource.Columns.AddRange([
            new TextColumn<UserData, string>("Gender", x => x.Gender),
            new TextColumn<UserData, string>("First Name", x => x.FirstName),
            new TextColumn<UserData, string>("Last Name", x => x.LastName),
            new TextColumn<UserData, string>("Username", x => x.UserName),
            new TextColumn<UserData, string>("E-Mail", x => x.EMail)]);
    }
}