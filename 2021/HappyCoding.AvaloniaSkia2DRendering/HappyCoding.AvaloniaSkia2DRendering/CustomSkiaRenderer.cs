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

        private SKPaint? _background;

        /// <inheritdoc />
        public Rect Bounds { get; set; }

        public CustomSkiaRenderer()
        {
            this.Bounds = Rect.Empty;

            _background = new SKPaint()
            {
                Color = new SKColor(150, 200, 255),
                Style = SKPaintStyle.Fill
            };
            _disposed = false;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _background?.Dispose();
            _background = null;

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

            var skiaCanvas = skiaContext.SkCanvas;
            skiaCanvas.Save();
            try
            {
                skiaCanvas.DrawRect(
                    new SKRect(5f, 5f, (float)this.Bounds.Width - 10, (float)this.Bounds.Height - 10),
                    _background);
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
