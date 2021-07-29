using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace HappyCoding.BlazorWith3D.BabylonJS
{
    public partial class CanvasBabylon
    {
        public Guid CanvasGuid { get; } = Guid.NewGuid();

        [Inject]
        public BabylonJSInterop BabylonJSInterop { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await this.BabylonJSInterop.InitCanvasAsync(this.CanvasGuid.ToString());
            }

            await base.OnAfterRenderAsync(firstRender);
        }
    }
}
