using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace HappyCoding.BlazorWith3D.ThreeJS
{
    public static class StartupExtensions
    {
        public static void AddThreeJSServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ThreeJSInterop>();
        }
    }
}
