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
        var dbConnectionString = "Data Source=localhost,1433;Initial Catalog=HappyCoding_2023_EFCoreOwnedTypes;User Id=SA;Password=DasIstEinGeheimes@Passwort123?;Trusted_Connection=False;Encrypt=False;";

        await MigrateDatabaseAsync(dbConnectionString);
        await DeletingPreviousTestDataAsync(dbConnectionString);
        await CreateNewTestDataAsync(dbConnectionString);
        await QuerySomeDataAsync(dbConnectionString);
    }

    private static async Task MigrateDatabaseAsync(string dbConnectionString)
    {
        Console.WriteLine("Migrating database...");
        await DbUtil.MigrateDbAsync(dbConnectionString);
    }

    private static async Task DeletingPreviousTestDataAsync(string dbConnectionString)
    {
        Console.WriteLine("Deleting previous test data...");

        await using var dbContext = DbUtil.CreateDbContext(dbConnectionString);

        await dbContext.Companies.ExecuteDeleteAsync();
        await dbContext.Persons.ExecuteDeleteAsync();
    }

    public static async Task CreateNewTestDataAsync(string dbConnectionString)
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

    public static async Task QuerySomeDataAsync(string dbConnectionString)
    {
        Console.WriteLine("Selecting some data...");
        
        await using var dbContext = DbUtil.CreateDbContext(dbConnectionString, true);

        Console.WriteLine();
        Console.WriteLine("### All persons, order by postal code");
        var personsByPostalCode = await dbContext.Persons
            .OrderBy(x => x.Address.PostalCode)
            .ToArrayAsync();

        Console.WriteLine();
        Console.WriteLine("### All companies, order by postal code of secondary address");
        var companiesByPostalCode = await dbContext.Companies
            .OrderBy(x => x.SecondaryAddress.PostalCode)
            .ToArrayAsync();

        Console.WriteLine();
        var cityName = personsByPostalCode[3].Address.City;
        Console.WriteLine($"### All persons in city '{cityName}', order by last name");
        var personsInCity = await dbContext.Persons
            .Where(x => x.Address.City == cityName)
            .OrderBy(x => x.LastName)
            .ToArrayAsync();
    }
}
