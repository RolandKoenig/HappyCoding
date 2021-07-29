/**
 * Interop code for ThreeJSInterop.cs
 * Based on sample from https://github.com/mrdoob/three.js/
 */

class ThreeJSInterop {

    constructor() {
        this.scene = null;
        this.camera = null;
        this.mesh = null;
        this.renderer = null;
    }

    /**
     * Gets the version of Three.js
     */
    getVersion() {
        return THREE.REVISION;
    }

    /**
     * Initializes a 3d view for a canvas with the given id
     * @param {any} canvasId
     */
    initCanvas(canvasId) {
        const threeCanvas = document.getElementById(canvasId);

        this.camera = new THREE.PerspectiveCamera(70, threeCanvas.clientWidth / threeCanvas.clientHeight, 0.01, 10);
        this.camera.position.z = 1;

        this.scene = new THREE.Scene();

        const geometry = new THREE.BoxGeometry(0.2, 0.2, 0.2);
        const material = new THREE.MeshNormalMaterial();

        this.mesh = new THREE.Mesh(geometry, material);
        this.scene.add(this.mesh);

        this.renderer = new THREE.WebGLRenderer({
            antialias: true,
            canvas: threeCanvas
        });
        this.renderer.setSize(threeCanvas.clientWidth, threeCanvas.clientHeight );
        this.renderer.setAnimationLoop((time) => this.perFrameAnimation(time));
    }

    /**
     * Gets called x times per second. It triggers animation calculation and rendering.
     * @param {any} time
     */
    perFrameAnimation(time) {
        this.mesh.rotation.x = time / 2000;
        this.mesh.rotation.y = time / 1000;

        this.renderer.render(this.scene, this.camera);
    }
}

// Export interop object
export const threeJSInterop = new ThreeJSInterop();