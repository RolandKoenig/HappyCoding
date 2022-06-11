using Microsoft.UI.Xaml;
using System;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Windows.Foundation;
using Microsoft.UI.Xaml.Input;
using SeeingSharp.Components.Input;
using SeeingSharp.Core;
using SeeingSharp.Core.Animations;
using SeeingSharp.Drawing2D.Resources;
using SeeingSharp.Drawing3D;
using SeeingSharp.Drawing3D.Primitives;
using SeeingSharp.Drawing3D.Resources;
using SeeingSharp.Mathematics;

namespace HappyCoding.SeeingSharp2WithPicking
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private TextFormatResource? _textFormat;
        private BrushResource? _textBrush;

        private bool _pointerInside;
        private Point _currentPointerLocation;
        private bool _sceneLoaded;
        private SceneObject? _pickedObject;

        public MainWindow()
        {
            this.InitializeComponent();

            _textFormat = new TextFormatResource("Arial", 16);
            _textBrush = new SolidBrushResource(Color4.White);

            this.Activated += OnActivated;
            this.Closed += OnClosed;
            
            this.CtrlView3D.PointerEntered += OnCtrlView3D_PointerEntered;
            this.CtrlView3D.PointerExited += OnCtrlView3D_PointerExited;
            this.CtrlView3D.PointerMoved += OnCtrlView3D_PointerMoved;
        }

        private async void RunObjectPickingLoop()
        {
            while (true)
            {
                if (_pointerInside)
                {
                    var objectsBelowCursor =
                        await this.CtrlView3D.PickObjectAsync(_currentPointerLocation, new PickingOptions());
                    _pickedObject = objectsBelowCursor?.FirstOrDefault();
                }
                else
                {
                    _pickedObject = null;
                    await Task.Delay(500);
                }
            }
        }

        private void OnCtrlView3D_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            _currentPointerLocation = e.GetCurrentPoint(this.CtrlView3D).Position;
        }

        private void OnCtrlView3D_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            _pointerInside = false;
        }

        private void OnCtrlView3D_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            _pointerInside = true;
        }

        private async void OnActivated(object sender, WindowActivatedEventArgs args)
        {
            if (_sceneLoaded)
            {
                return;
            }
            _sceneLoaded = true;

            // Build the scene
            var scene = this.CtrlView3D.Scene;
            await scene.ManipulateSceneAsync(manipulator =>
            {
                // Create material resource
                var resMaterial = manipulator.AddStandardMaterialResource();
                var resGeometry = manipulator.AddResource(
                    _ => new GeometryResource(new CubeGeometryFactory()));

                // Create the mesh and animate it
                var mesh = new Mesh(resGeometry, resMaterial);
                mesh.Position = new Vector3(0f, 1f, 0f);
                mesh.Color = Color4.GreenColor;
                mesh.BuildAnimationSequence()
                    .RotateEulerAnglesTo(new Vector3(0f, EngineMath.RAD_180DEG, 0f), TimeSpan.FromSeconds(2.0))
                    .WaitFinished()
                    .RotateEulerAnglesTo(new Vector3(0f, EngineMath.RAD_360DEG, 0f), TimeSpan.FromSeconds(2.0))
                    .WaitFinished()
                    .CallAction(() => mesh.RotationEuler = Vector3.Zero)
                    .ApplyAndRewind();
                manipulator.AddObject(mesh);
            });

            // Draw currently picked object on the screen
            await this.CtrlView3D.RenderLoop.Register2DDrawingLayerAsync(graphics =>
            {
                var textFormat = _textFormat;
                var textBrush = _textBrush;
                if ((textFormat == null) || (textBrush == null))
                {
                    return;
                }

                graphics.DrawText(
                    "Picked object: " + _pickedObject, 
                    textFormat,
                    new System.Drawing.RectangleF(10f, 10f, graphics.ScreenWidth - 20f, 30f),
                    textBrush);
            });

            // Configure camera
            var camera = this.CtrlView3D.Camera;
            camera.Position = new Vector3(3f, 3f, 3f);
            camera.Target = new Vector3(0f, 0.5f, 0f);
            camera.UpdateCamera();

            // Append camera behavior (this enables simple input / movement)
            this.CtrlView3D.RenderLoop.SceneComponents.Add(new FreeMovingCameraComponent());

            RunObjectPickingLoop();
        }

        private void OnClosed(object sender, WindowEventArgs args)
        {
            _textBrush?.Dispose();
            _textFormat?.Dispose();
            _textBrush = null;
            _textFormat = null;
        }
    }
}
