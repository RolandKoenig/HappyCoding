using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using HappyCoding.AvaloniaMarkdownHelpBrowser.Util;
using Markdown.Avalonia.Utils;

namespace HappyCoding.AvaloniaMarkdownHelpBrowser
{
    public class ResourceBitmapLoader : IBitmapLoader
    {
        /// <inheritdoc />
        public string? AssetPathRoot { get; set; }

        private Assembly _assetAssembly;
        private ConcurrentDictionary<string, WeakReference<Bitmap>> _bitmapCache;

        public ResourceBitmapLoader(Assembly assetAssembly)
        {
            _assetAssembly = assetAssembly;
            this.AssetPathRoot = string.Empty;

            this._bitmapCache = new ConcurrentDictionary<string, WeakReference<Bitmap>>();
        }

        private void Compact()
        {
            foreach (var entry in _bitmapCache.ToArray())
            {
                if (!entry.Value.TryGetTarget(out var dummy))
                {
                    ((IDictionary<string, WeakReference<Bitmap>>)_bitmapCache).Remove(entry.Key);
                }
            }
        }

        public static string BuildEmbeddedResourceName(string defaultNamespace, string assetPathRoot, string urlText)
        {
            var strBuilder = PooledStringBuilders.Current.TakeStringBuilder();
            try
            {
                if (defaultNamespace.Length > 0)
                {
                    strBuilder.Append(defaultNamespace);
                    strBuilder.Append('.');
                }

                if (assetPathRoot.Length > 0)
                {
                    ReplacePathCharactersForEmbeddedResource(assetPathRoot, strBuilder);
                    strBuilder.Append('.');
                }

                ReplacePathCharactersForEmbeddedResource(urlText, strBuilder);

                return strBuilder.ToString();
            }
            finally
            {
                PooledStringBuilders.Current.ReRegisterStringBuilder(strBuilder);
            }
        }

        public static void ReplacePathCharactersForEmbeddedResource(string url, StringBuilder target)
        {
            for (var loop = 0; loop < url.Length; loop++)
            {
                var actChar = url[loop];
                switch (actChar)
                {
                    case '\\':
                    case '/':
                        target.Append('.');
                        break;

                    default:
                        target.Append(actChar);
                        break;
                }
            }
        }

        public Bitmap? Get(string urlTxt)
        {
            var assetPathRoot = this.AssetPathRoot ?? string.Empty;
            var embeddedResourceName = BuildEmbeddedResourceName(
                _assetAssembly.GetName().Name ?? string.Empty,
                assetPathRoot, 
                urlTxt);

            if (_bitmapCache.TryGetValue(embeddedResourceName, out var reference))
            {
                if (reference.TryGetTarget(out var cachedBitmap))
                {
                    return cachedBitmap;
                }
            }

            using var inStream = _assetAssembly.GetManifestResourceStream(embeddedResourceName);
            if (inStream != null)
            {
                var newBitmap = new Bitmap(inStream);
                _bitmapCache[embeddedResourceName] = new WeakReference<Bitmap>(newBitmap);
                return newBitmap;
            }

            return null;
        }
    }
}
