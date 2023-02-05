using Bogus;
using Bogus.DataSets;

namespace HappyCoding.BlazorComponentLibs.Shared.Data;

public class UserData
{
    public string Gender { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string UserName { get; set; } = string.Empty;

    public string EMail { get; set; } = string.Empty;

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
