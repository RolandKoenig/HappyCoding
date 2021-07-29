using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace HappyCoding.BlazorWith3D.ThreeJS
{
    public partial class CanvasThreeJS
    {
        public Guid CanvasGuid { get; } = Guid.NewGuid();

        public string ThreeJSVersion { get; set; } = string.Empty;

        [Inject]
        public ThreeJSInterop ThreeJSInterop { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await this.ThreeJSInterop.InitCanvasAsync(this.CanvasGuid.ToString());

                this.ThreeJSVersion = await this.ThreeJSInterop.GetVersionAsync();
            }

            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                // Trigger rendering again because after initialization we know more about Three.js (like the version)
                this.StateHasChanged();
            }
        }
    }
}
