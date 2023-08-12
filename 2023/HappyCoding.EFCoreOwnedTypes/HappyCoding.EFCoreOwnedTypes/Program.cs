using Bogus;
using HappyCoding.EFCoreOwnedTypes;
using HappyCoding.EFCoreOwnedTypes.Data;
using Microsoft.EntityFrameworkCore;

using Person = HappyCoding.EFCoreOwnedTypes.Data.Person;

namespace ConsoleApp1;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var dbConnectionString = "Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=HappyCoding_2023_EFCoreOwnedTypes;Integrated Security=SSPI";

        await MigrateDatabaseAsync(dbConnectionString);
        await DeletingPreviousTestData(dbConnectionString);
        await CreateNewTestData(dbConnectionString);



    }

    private static async Task MigrateDatabaseAsync(string dbConnectionString)
    {
        Console.WriteLine("Migrating database...");
        await DbUtil.MigrateDbAsync(dbConnectionString);
    }

    private static async Task DeletingPreviousTestData(string dbConnectionString)
    {
        Console.WriteLine("Deleting previous test data...");

        await using var dbContext = DbUtil.CreateDbContext(dbConnectionString);

        await dbContext.Companies.ExecuteDeleteAsync();
        await dbContext.Persons.ExecuteDeleteAsync();
    }

    public static async Task CreateNewTestData(string dbConnectionString)
    {
        Console.WriteLine("Creating new test data...");

        await using var dbContext = DbUtil.CreateDbContext(dbConnectionString);

        var personFaker = new Faker<Person>()
            .RuleFor(x => x.FirstName, (f) => f.Name.FirstName())
            .RuleFor(x => x.LastName, f => f.Name.LastName())
            .RuleFor(x => x.Address, f => new Address()
            {
                City = f.Address.City(),
                PostalCode = f.Address.ZipCode(),
                Street = f.Address.StreetName(),
                HouseNumber = f.Address.BuildingNumber()
            });
        await dbContext.Persons.AddRangeAsync(personFaker.Generate(50));

        var companyFaker = new Faker<Company>()
            .RuleFor(x => x.Name, f => f.Company.CompanyName())
            .RuleFor(x => x.Address, f => new Address()
            {
                City = f.Address.City(),
                PostalCode = f.Address.ZipCode(),
                Street = f.Address.StreetName(),
                HouseNumber = f.Address.BuildingNumber()
            })
            .RuleFor(x => x.SecondaryAddress, f => new Address()
            {
                City = f.Address.City(),
                PostalCode = f.Address.ZipCode(),
                Street = f.Address.StreetName(),
                HouseNumber = f.Address.BuildingNumber()
            });
        await dbContext.Companies.AddRangeAsync(companyFaker.Generate(50));

        await dbContext.SaveChangesAsync();
    }
}
