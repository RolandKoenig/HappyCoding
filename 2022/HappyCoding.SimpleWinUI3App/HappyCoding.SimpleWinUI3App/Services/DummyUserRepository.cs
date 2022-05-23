using System.Collections.Generic;
using System.Threading.Tasks;
using HappyCoding.SimpleWinUI3App.Model;

namespace HappyCoding.SimpleWinUI3App.Services;

public class DummyUserRepository : IUserRepository
{
    public async Task<IEnumerable<UserInfo>> SearchUsersAsync(string searchString)
    {
        await Task.Delay(2000);

        var result = new List<UserInfo>();
        result.Add(new UserInfo()
        {
            Name = "Manuela Diederich",
            Age = 33,
            HomeTown = "Landsberg"
        });
        result.Add(new UserInfo()
        {
            Name = "Sarah Huber",
            Age = 20,
            HomeTown = "Völklingen"
        });
        result.Add(new UserInfo()
        {
            Name = "Thorsten Cole",
            Age = 74,
            HomeTown = "Geilnau"
        });
        result.Add(new UserInfo()
        {
            Name = "Bernd Holtzmann",
            Age = 26,
            HomeTown = "Westerland"
        });
        result.Add(new UserInfo()
        {
            Name = "Roland König",
            Age = 34,
            HomeTown = "Erlangen"
        });

        return result;
    }
}
