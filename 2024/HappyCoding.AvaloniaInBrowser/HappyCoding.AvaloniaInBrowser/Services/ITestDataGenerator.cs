using System.Collections.Generic;
using HappyCoding.AvaloniaInBrowser.Data;

namespace HappyCoding.AvaloniaInBrowser.Services;

public interface ITestDataGenerator
{
    public IEnumerable<UserData> GenerateUserData(int countRows);
}