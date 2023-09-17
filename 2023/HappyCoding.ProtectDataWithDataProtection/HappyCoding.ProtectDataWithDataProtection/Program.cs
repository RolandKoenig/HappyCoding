// See https://aka.ms/new-console-template for more information

using HappyCoding.ProtectDataWithDataProtection;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;

public static class Program
{
    public static void Main(string[] args)
    {
        var services = new ServiceCollection();
        services.AddDataProtection(options => options.ApplicationDiscriminator = "HappyCoding.ProtectDataWithDataProtection")
           .PersistKeysToFileSystem(new DirectoryInfo("./keys"));
        services.AddTransient<LogicClassContainingProtector>();
        var serviceProvider = services.BuildServiceProvider();
        
        while (true)
        {
            Console.WriteLine("################# New run");
            
            var logicClassContainingProtector = serviceProvider.GetRequiredService<LogicClassContainingProtector>();
            Console.Write("Data to protect: ");
            var dataToProtect = Console.ReadLine() ?? "";

            string protectedData;
            if (!string.IsNullOrEmpty(dataToProtect))
            {
                protectedData = logicClassContainingProtector.ProtectSomeData(dataToProtect);
                Console.WriteLine($"Protected: {protectedData}");
            }
            else
            {
                Console.Write("Data to unprotect: ");
                protectedData = Console.ReadLine() ?? "";
            }

            var unprotectedData = logicClassContainingProtector.UnprotectSomeData(protectedData);
            Console.WriteLine($"Unprotected: {unprotectedData}");
            
            Console.WriteLine();
        }
    }
}
