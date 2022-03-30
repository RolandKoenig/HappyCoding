using Light.GuardClauses;

namespace HappyCoding.CQSWithMediatR.Domain.Model;

public class Person
{
    public string Name { get; private set; }

    public string Address { get; private set; }

    public Person(string name, string address)
    {
        name.MustNotBeNullOrWhiteSpace();
        address.MustNotBeNullOrWhiteSpace();

        this.Name = name;
        this.Address = address;
    }
}