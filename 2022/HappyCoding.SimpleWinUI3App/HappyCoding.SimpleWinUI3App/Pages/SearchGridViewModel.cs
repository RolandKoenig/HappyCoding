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

            if (!string.IsNullOrEmpty(this.SearchString))
            {
                this.UserInfo.Add(new UserInfo()
                {
                    Name = "Manuela Diederich",
                    Age = 33,
                    HomeTown = "Landsberg"
                });
                this.UserInfo.Add(new UserInfo()
                {
                    Name = "Sarah Huber",
                    Age = 20,
                    HomeTown = "Völklingen"
                });
                this.UserInfo.Add(new UserInfo()
                {
                    Name = "Thorsten Cole",
                    Age = 74,
                    HomeTown = "Geilnau"
                });
                this.UserInfo.Add(new UserInfo()
                {
                    Name = "Bernd Holtzmann",
                    Age = 26,
                    HomeTown = "Westerland"
                });
                this.UserInfo.Add(new UserInfo()
                {
                    Name = "Roland König",
                    Age = 34,
                    HomeTown = "Erlangen"
                });
            }
        }
        finally
        {
            this.IsSearching = false;
        }
    }
}
