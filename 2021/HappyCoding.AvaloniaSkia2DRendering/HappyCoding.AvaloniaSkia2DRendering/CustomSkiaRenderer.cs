using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Platform;
using Avalonia.Rendering.SceneGraph;
using Avalonia.Skia;
using SkiaSharp;

namespace HappyCoding.AvaloniaSkia2DRendering
{
    public class CustomSkiaRenderer : ICustomDrawOperation, IDisposable
    {
        private bool _disposed;
        private Rect _bounds;
        private bool _boundsChanged;
        private object _renderDisposeLock;

        // Size independent resources
        private SKPaint _backgroundPaint;
        private SKPaint _rectBorderPaint;

        // Size dependent resources
        private SKShader? _backgroundShader;

        /// <inheritdoc />
        public Rect Bounds
        {
            get => _bounds;
            set
            {
                if (_bounds != value)
                {
                    _bounds = value;
                    _boundsChanged = true;
                }
            }
        }

        public CustomSkiaRenderer()
        {
            this.Bounds = Rect.Empty;

            _backgroundPaint = new SKPaint()
            {
                Shader = _backgroundShader,
                Style = SKPaintStyle.Fill,
                IsAntialias = true
            };
            _rectBorderPaint = new SKPaint()
            {
                Color = SKColors.Black, 
                Style = SKPaintStyle.Stroke
            };

            this.RecreateSizeDependentResources();

            _disposed = false;
            _renderDisposeLock = new object();
        }

        private void RecreateSizeDependentResources()
        {
            var height = (float)_bounds.Height;
            if (height < 1f) { height = 1f;}

            _backgroundShader?.Dispose();
            _backgroundShader = SKShader.CreateLinearGradient(
                new SKPoint(0, 0),
                new SKPoint(0, height),
                new SKColorF[] {SKColors.LightBlue, SKColors.SteelBlue},
                null,
                SKShaderTileMode.Repeat);
            _backgroundPaint.Shader = _backgroundShader;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            lock (_renderDisposeLock)
            {
                _disposed = true;

                // Size independent resources
                _backgroundPaint.Dispose();

                // Size dependent resources
                _backgroundShader?.Dispose();
            }
        }

        /// <inheritdoc />
        public bool HitTest(Point p)
        {
            if(_bounds.Contains(p))
            {
                Debug.WriteLine($"HitTest True: {p}");
            }
            else
            {
                Debug.WriteLine($"HitTest False: {p}");
            }


            return false;
        }

        /// <inheritdoc />
        public void Render(IDrawingContextImpl context)
        {
            lock (_renderDisposeLock)
            {
                if (_disposed){ return; }
                if (_bounds == Rect.Empty) { return; }

                if (!(context is ISkiaDrawingContextImpl skiaContext))
                {
                    // Cancel here...
                    // We do only support custom rendering with Skia
                    return;
                }

                if (_boundsChanged)
                {
                    this.RecreateSizeDependentResources();
                    _boundsChanged = false;
                }

                var skiaCanvas = skiaContext.SkCanvas;
                skiaCanvas.Save();
                try
                {
                    var skiaBounds = new SKRect(
                        0f, 0f,
                        (float)_bounds.Width, (float)_bounds.Height);

                    // Draw background
                    skiaCanvas.DrawRect(skiaBounds, _backgroundPaint);

                    for (var actX = skiaBounds.Left + 5f; actX < skiaBounds.Width; actX += 30f)
                    {
                        for (var actY = skiaBounds.Top + 5f; actY < skiaBounds.Height; actY += 30f)
                        {
                            skiaCanvas.DrawRect(
                                actX, actY, 20f, 20f,
                                _rectBorderPaint);
                        }
                    }
                }
                finally
                {
                    skiaCanvas.Restore();
                }
            }
        }

        /// <inheritdoc />
        public bool Equals(ICustomDrawOperation? other)
        {
            return ReferenceEquals(this, other);
        }
    }
}
