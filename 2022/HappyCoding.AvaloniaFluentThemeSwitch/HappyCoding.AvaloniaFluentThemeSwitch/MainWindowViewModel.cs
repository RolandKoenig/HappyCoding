using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Bogus;
using Bogus.DataSets;

namespace HappyCoding.AvaloniaFluentThemeSwitch;

internal class MainWindowViewModel
{
    public List<UserData> UserData { get; }

    public MainWindowViewModel()
    {
        if (Design.IsDesignMode)
        {
            this.UserData = new List<UserData>();
        }
        else
        {
            var testUserGenerator = new Faker<UserData>()
                .RuleFor(u => u.Gender, f => f.PickRandom<Name.Gender>().ToString())
                .RuleFor(u => u.FirstName, (f, u) => f.Name.FirstName(Enum.Parse<Name.Gender>(u.Gender)))
                .RuleFor(u => u.LastName, (f, u) => f.Name.LastName(Enum.Parse<Name.Gender>(u.Gender)))
                .RuleFor(u => u.UserName, (f, u) => f.Internet.UserName(u.FirstName, u.LastName))
                .RuleFor(u => u.EMail, (f, u) => f.Internet.Email(u.FirstName, u.LastName));

            this.UserData = new List<UserData>(
                Enumerable.Range(0, 50).Select(_ => testUserGenerator.Generate()));
        }
    }
}
