using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace HappyCoding.BlazorWith3D.BabylonJS
{
    /// <summary>
    /// Interop code for babylonJSInterop.js
    /// </summary>
    public class BabylonJSInterop : IAsyncDisposable
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly ValueTask _loadBabylonTask;
        private readonly Lazy<ValueTask<IJSObjectReference>> _moduleTask;

        public BabylonJSInterop(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
            _loadBabylonTask = jsRuntime.InvokeVoidAsync("import", "https://cdn.babylonjs.com/babylon.js");

            _moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
               "import", "./_content/HappyCoding.BlazorWith3D.BabylonJS/babylonJSInterop.js"));
        }

        public async ValueTask InitCanvasAsync(string canvasID)
        {
            await _loadBabylonTask;
            var module = await _moduleTask.Value;

            await module.InvokeVoidAsync("babylonJSInterop.initCanvas", canvasID);
        }

        public async ValueTask DisposeAsync()
        {
            await _loadBabylonTask;

            if (_moduleTask.IsValueCreated)
            {
                var module = await _moduleTask.Value;
                await module.DisposeAsync();
            }
        }
    }
}
