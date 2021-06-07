using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.LogicalTree;
using Avalonia.Media;
using Avalonia.Threading;

namespace HappyCoding.AvaloniaSkia2DRendering
{
    public class CustomSkiaRendererControl : Control
    {
        private CustomSkiaRenderer? _skiaRenderer;

        public override void Render(DrawingContext context)
        {
            base.Render(context);

            if (_skiaRenderer != null)
            {
                _skiaRenderer.Bounds = this.Bounds;
                context.Custom(_skiaRenderer);
            }

            Dispatcher.UIThread.InvokeAsync(
                this.InvalidateVisual,
                DispatcherPriority.Normal);
        }

        /// <inheritdoc />
        protected override void OnAttachedToLogicalTree(LogicalTreeAttachmentEventArgs e)
        {
            base.OnAttachedToLogicalTree(e);

            _skiaRenderer ??= new CustomSkiaRenderer();
        }

        /// <inheritdoc />
        protected override void OnDetachedFromLogicalTree(LogicalTreeAttachmentEventArgs e)
        {
            base.OnDetachedFromLogicalTree(e);

            _skiaRenderer?.Dispose();
            _skiaRenderer = null;
        }
    }
}
