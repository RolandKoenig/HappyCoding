using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using Bogus.DataSets;
using HappyCoding.AvaloniaTreeDataGrid.Data;

namespace HappyCoding.AvaloniaTreeDataGrid.Services;

public class BogusTestDataGenerator : ITestDataGenerator
{
    /// <inheritdoc />
    public IEnumerable<UserData> GenerateUserData(int countRows)
    {
        var plainUserGenerator = new Faker<UserData>()
            .RuleFor(u => u.Gender, f => f.PickRandom<Name.Gender>().ToString())
            .RuleFor(u => u.FirstName, (f, u) => f.Name.FirstName(Enum.Parse<Name.Gender>(u.Gender)))
            .RuleFor(u => u.LastName, (f, u) => f.Name.LastName(Enum.Parse<Name.Gender>(u.Gender)))
            .RuleFor(u => u.UserName, (f, u) => f.Internet.UserName(u.FirstName, u.LastName))
            .RuleFor(u => u.EMail, (f, u) => f.Internet.Email(u.FirstName, u.LastName));
        
        var random = new Random();
        var result = Enumerable.Range(0, countRows).Select(_ => plainUserGenerator.Generate()).ToList();
        foreach (var actUser in result)
        {
            var countChilds = random.Next(0, 4);
            if(countChilds == 0){ continue; }
            
            actUser.Children =
                Enumerable.Range(0, countChilds).Select(_ => plainUserGenerator.Generate()).ToList();
        }

        return result;
    }
}