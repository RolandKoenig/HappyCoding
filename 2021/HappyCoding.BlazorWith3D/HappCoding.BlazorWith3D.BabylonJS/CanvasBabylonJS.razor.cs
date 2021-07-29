using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace HappyCoding.BlazorWith3D.BabylonJS
{
    public partial class CanvasBabylonJS : IAsyncDisposable
    {
        public Guid CanvasGuid { get; } = Guid.NewGuid();

        public string BabylonJSVersion { get; set; } = string.Empty;

        [Inject]
        public BabylonJSInterop BabylonJSInterop { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await this.BabylonJSInterop.InitCanvasAsync(this.CanvasGuid.ToString());

                this.BabylonJSVersion = await this.BabylonJSInterop.GetVersionAsync();
            }

            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                this.StateHasChanged();
            }
        }

        public ValueTask DisposeAsync()
        {
            return this.BabylonJSInterop.UnloadCanvasAsync();
        }
    }
}
