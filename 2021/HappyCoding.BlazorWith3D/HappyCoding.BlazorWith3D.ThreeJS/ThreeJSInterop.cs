using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace HappyCoding.BlazorWith3D.ThreeJS
{
    public class ThreeJSInterop : IAsyncDisposable
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask;

        public ThreeJSInterop(IJSRuntime jsRuntime)
        {
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
               "import", "./_content/HappyCoding.BlazorWith3D.ThreeJS/exampleJsInterop.js").AsTask());
        }

        public ValueTask InitCanvasAsync(string canvasID)
        {
            return ValueTask.CompletedTask;
            //await _loadBabylonTask;
            //var module = await _moduleTask.Value;

            //await module.InvokeVoidAsync("babylonJSInterop.initCanvas", canvasID);
        }

        public async ValueTask DisposeAsync()
        {
            if (moduleTask.IsValueCreated)
            {
                var module = await moduleTask.Value;
                await module.DisposeAsync();
            }
        }
    }
}
