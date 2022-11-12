using HappyCoding.HexagonalArchitecture.WebUI.Client;
using HappyCoding.HexagonalArchitecture.WebUI.Client.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace HappyCoding.HexagonalArchitecture.WebUI.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped<IWorkshopClient, WorkshopClient>();

            await builder.Build().RunAsync();
        }
    }
}