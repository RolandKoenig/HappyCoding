/**
 * Interop code for BabylonJSInterop.cs
 * Based on sample code from https://darnton.co.nz/2020/07/23/3d-blazor-with-babylon/
 */

class BabylonJSInterop {

    constructor() {
        this.babylonEngine = null;
        this.scene = null;
        this.camera = null;
        this.light1 = null;
        this.light2 = null;
        this.sphere = null;
    }

    /**
     * Initializes a 3d view for a canvas with the given id
     * @param {any} canvasId
     */
    initCanvas(canvasId) {
        const babylonCanvas = document.getElementById(canvasId);

        this.babylonEngine = new BABYLON.Engine(babylonCanvas, true);
        this.scene = new BABYLON.Scene(this.babylonEngine);

        this.camera = new BABYLON.ArcRotateCamera("Camera", Math.PI / 2, Math.PI / 2, 2, new BABYLON.Vector3(0, 0, 5), this.scene);
        this.camera.attachControl(babylonCanvas, true);
 
        this.light1 = new BABYLON.HemisphericLight("light1", new BABYLON.Vector3(1, 1, 0), this.scene);
        this.light2 = new BABYLON.PointLight("light2", new BABYLON.Vector3(0, 1, -1), this.scene);
 
        this.sphere = BABYLON.MeshBuilder.CreateSphere("sphere", { diameter: 2 }, this.scene);

        this.babylonEngine.runRenderLoop(() => this.perFrameAnimation());
    }

    getVersion() {
        return BABYLON.Engine.Version;
    }

    /**
    * Gets called x times per second. It triggers animation calculation and rendering.
    */
    perFrameAnimation() {
        this.scene.render();
    }

    /**
     * Disposes all resources allocated by babylon.js
     */
    unloadCanvas() {
        this.sphere.dispose();

        this.light2.dispose();
        this.light1.dispose();

        this.babylonEngine.dispose();
    }
}

// Export interop object
export const babylonJSInterop = new BabylonJSInterop();