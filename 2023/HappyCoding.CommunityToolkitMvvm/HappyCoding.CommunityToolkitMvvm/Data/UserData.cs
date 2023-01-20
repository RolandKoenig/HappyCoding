using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Bogus;
using Bogus.DataSets;
using CommunityToolkit.Mvvm.ComponentModel;

namespace HappyCoding.CommunityToolkitMvvm.Data;

public partial class UserData : ObservableObject
{
    [ObservableProperty]
    private string _gender = string.Empty;
    
    [ObservableProperty]
    private string _firstName = string.Empty;

    [ObservableProperty]
    private string _lastName = string.Empty;

    [ObservableProperty]
    [property: Required]
    private string _userName = string.Empty;

    [ObservableProperty] 
    [property: Required]
    private string _eMail = string.Empty;

    public static IEnumerable<UserData> CreateFakeData(int count)
    {
        var testUserGenerator = new Faker<UserData>()
            .RuleFor(u => u.Gender, f => f.PickRandom<Name.Gender>().ToString())
            .RuleFor(u => u.FirstName, (f, u) => f.Name.FirstName(Enum.Parse<Name.Gender>(u.Gender)))
            .RuleFor(u => u.LastName, (f, u) => f.Name.LastName(Enum.Parse<Name.Gender>(u.Gender)))
            .RuleFor(u => u.UserName, (f, u) => f.Internet.UserName(u.FirstName, u.LastName))
            .RuleFor(u => u.EMail, (f, u) => f.Internet.Email(u.FirstName, u.LastName));

        return Enumerable.Range(0, count).Select(_ => testUserGenerator.Generate());
    }
}