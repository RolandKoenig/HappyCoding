using Avalonia.Controls;
using Avalonia.Controls.Models.TreeDataGrid;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HappyCoding.AvaloniaTreeDataGrid.Data;
using HappyCoding.AvaloniaTreeDataGrid.Services;

namespace HappyCoding.AvaloniaTreeDataGrid.Views;

public partial class HierarchicalDataViewModel : ObservableObject
{
    [ObservableProperty]
    private HierarchicalTreeDataGridSource<UserData> _dataSource;
    
    public HierarchicalDataViewModel(ITestDataGenerator testDataGenerator)
    {
        this.DataSource = new HierarchicalTreeDataGridSource<UserData>(testDataGenerator.GenerateUserData(50));
        this.DataSource.Columns.AddRange([
            new HierarchicalExpanderColumn<UserData>(
                new TextColumn<UserData, string>("Gender", x => x.Gender),
                x => x.Children),
            new TextColumn<UserData, string>("First Name", x => x.FirstName),
            new TextColumn<UserData, string>("Last Name", x => x.LastName),
            new TextColumn<UserData, string>("Username", x => x.UserName),
            new TextColumn<UserData, string>("E-Mail", x => x.EMail)]);
    }
}