/**
 * Interop code for BabylonJSInterop.cs
 * Based on sample code from https://darnton.co.nz/2020/07/23/3d-blazor-with-babylon/
 */

class BabylonJSInterop {

    /**
     * Initializes a 3d view for a canvas with the given id
     * @param {any} canvasId
     */
    initCanvas(canvasId) {
        const babylonCanvas = document.getElementById(canvasId);
        const babylonEngine = new BABYLON.Engine(babylonCanvas, true);
        const scene = this.createSceneWithSphere(babylonEngine, babylonCanvas);

        babylonEngine.runRenderLoop(function () {
            scene.render();
        });
    }

    /**
     * Creates the default scene
     * @param {any} engine
     * @param {any} canvas
     */
    createSceneWithSphere(engine, canvas) {
        const scene = new BABYLON.Scene(engine);

        const camera = new BABYLON.ArcRotateCamera("Camera", Math.PI / 2, Math.PI / 2, 2, new BABYLON.Vector3(0, 0, 5), scene);
        camera.attachControl(canvas, true);
 
        const light1 = new BABYLON.HemisphericLight("light1", new BABYLON.Vector3(1, 1, 0), scene);
        const light2 = new BABYLON.PointLight("light2", new BABYLON.Vector3(0, 1, -1), scene);
 
        const sphere = BABYLON.MeshBuilder.CreateSphere("sphere", { diameter: 2 }, scene);
 
        return scene;
    }
}

// Export interop object
export const babylonJSInterop = new BabylonJSInterop();