using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace HappyCoding.BlazorWith3D.BabylonJS
{
    public static class StartupExtensions
    {
        public static void AddBabylonJSServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<BabylonJSInterop>();
        }
    }
}
