using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        }

        private void RecreateSizeDependentResources()
        {
            var height = (float)this.Bounds.Height;
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
            _backgroundPaint.Dispose();
            _backgroundShader?.Dispose();

            _disposed = true;
        }

        /// <inheritdoc />
        public bool HitTest(Point p)
        {
            return false;
        }

        /// <inheritdoc />
        public void Render(IDrawingContextImpl context)
        {
            if (_disposed){ return; }
            if (this.Bounds == Rect.Empty) { return; }

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
                    (float)this.Bounds.X, (float)this.Bounds.Y,
                    (float)this.Bounds.Width, (float)this.Bounds.Height);

                // Draw background
                skiaCanvas.DrawRect(skiaBounds, _backgroundPaint);

                for(var actX = skiaBounds.Left + 5f; actX < this.Bounds.Width; actX += 30f)
                {
                    for(var actY = skiaBounds.Top + 5f; actY < this.Bounds.Height; actY += 30f)
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

        /// <inheritdoc />
        public bool Equals(ICustomDrawOperation? other)
        {
            return ReferenceEquals(this, other);
        }
    }
}
