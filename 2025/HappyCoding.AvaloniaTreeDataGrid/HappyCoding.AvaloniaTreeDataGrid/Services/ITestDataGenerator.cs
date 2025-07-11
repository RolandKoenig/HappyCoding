using System.Collections.Generic;
using HappyCoding.AvaloniaTreeDataGrid.Data;

namespace HappyCoding.AvaloniaTreeDataGrid.Services;

public interface ITestDataGenerator
{
    public IEnumerable<UserData> GenerateUserData(int countRows);
}