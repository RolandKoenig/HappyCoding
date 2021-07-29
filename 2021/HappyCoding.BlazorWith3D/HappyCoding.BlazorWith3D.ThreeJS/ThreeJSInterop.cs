using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace HappyCoding.BlazorWith3D.ThreeJS
{
    public class ThreeJSInterop : IAsyncDisposable
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly ValueTask _loadBabylonTask;
        private readonly Lazy<ValueTask<IJSObjectReference>> _moduleTask;

        public ThreeJSInterop(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
            _loadBabylonTask = jsRuntime.InvokeVoidAsync(
                "import", "./_content/HappyCoding.BlazorWith3D.ThreeJS/three.js");

            _moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
                "import", "./_content/HappyCoding.BlazorWith3D.ThreeJS/threeJSInterop.js"));
        }

        public async ValueTask InitCanvasAsync(string canvasID)
        {
            await _loadBabylonTask;
            var module = await _moduleTask.Value;

            await module.InvokeVoidAsync("threeJSInterop.initCanvas", canvasID);
        }

        public async Task<string> GetVersionAsync()
        {
            await _loadBabylonTask;
            var module = await _moduleTask.Value;

            return await module.InvokeAsync<string>("threeJSInterop.getVersion");
        }

        public async ValueTask UnloadCanvasAsync()
        {
            await _loadBabylonTask;
            var module = await _moduleTask.Value;

            await module.InvokeVoidAsync("threeJSInterop.unloadCanvas");
        }

        public async ValueTask DisposeAsync()
        {
            if (_moduleTask.IsValueCreated)
            {
                var module = await _moduleTask.Value;
                await module.DisposeAsync();
            }
        }
    }
}
