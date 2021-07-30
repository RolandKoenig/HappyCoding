using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HappyCoding.BlazorWith3D.BabylonJS;
using HappyCoding.BlazorWith3D.ThreeJS;

namespace HappyCoding.BlazorWith3D.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            // Services for connection with 3D engines
            builder.Services.AddBabylonJSServices();
            builder.Services.AddThreeJSServices();

            await builder.Build().RunAsync();
        }
    }
}
