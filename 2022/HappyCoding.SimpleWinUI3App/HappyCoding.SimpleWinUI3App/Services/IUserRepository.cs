using System.Collections.Generic;
using System.Threading.Tasks;
using HappyCoding.SimpleWinUI3App.Model;

namespace HappyCoding.SimpleWinUI3App.Services;

public interface IUserRepository
{
    Task<IEnumerable<UserInfo>> SearchUsersAsync(string searchString);
}
