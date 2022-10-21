namespace HappyCoding.HexagonalArchitecture.WebUI.Server;

public static class Program
{
    public static async Task<int> Main(string[] args)
    {
        await CreateHostBuilder(args).Build().RunAsync();

        return 0;
    }

    public static IHostBuilder CreateHostBuilder(params string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
                webBuilder.UseKestrel();
            });
    }
}