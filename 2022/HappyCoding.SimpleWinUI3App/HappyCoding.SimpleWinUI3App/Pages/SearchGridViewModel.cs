using System.Collections.ObjectModel;
using System.Threading.Tasks;
using HappyCoding.SimpleWinUI3App.Model;
using HappyCoding.SimpleWinUI3App.Util;

namespace HappyCoding.SimpleWinUI3App.Pages;

public class SearchGridViewModel : ViewModelBase
{
    private string _searchString = string.Empty;
    private bool _isSearching = false;

    public string SearchString
    {
        get => _searchString;
        set
        {
            if (_searchString != value)
            {
                _searchString = value;
                this.RaisePropertyChanged();
            }
        }
    }

    public bool IsSearching
    {
        get => _isSearching;
        set
        {
            if (_isSearching != value)
            {
                _isSearching = value;
                this.RaisePropertyChanged();
                this.RaisePropertyChanged(nameof(this.IsInputPossible));
            }
        }
    }

    public bool IsInputPossible => !this.IsSearching;

    public ObservableCollection<UserInfo> UserInfo { get; } = new ObservableCollection<UserInfo>();

    public void ResetSearch()
    {
        this.SearchString = string.Empty;
        this.UserInfo.Clear();
    }

    public async Task SearchAsync()
    {
        this.IsSearching = true;
        try
        {
            this.UserInfo.Clear();

            await Task.Delay(2000);
        }
        finally
        {
            this.IsSearching = false;
        }
    }
}
