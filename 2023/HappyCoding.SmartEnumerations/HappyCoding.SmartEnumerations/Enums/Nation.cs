using Ardalis.SmartEnum;

namespace HappyCoding.SmartEnumerations.Enums;

public class Nation : SmartEnum<Nation, string>
{
    public static readonly Nation Germany = new Nation("Germany", "DE");
    public static readonly Nation France = new Nation("France", "FR");
    public static readonly Nation Austria = new Nation("Austria", "AT");

    // public static readonly Nation Default = Germany;

    private Nation()
        : base(GetDefault().Name, GetDefault().Value)
    {

    }

    private Nation(string name, string value)
        : base(name, value)
    {

    }

    public static Nation GetDefault() => Germany;
}
