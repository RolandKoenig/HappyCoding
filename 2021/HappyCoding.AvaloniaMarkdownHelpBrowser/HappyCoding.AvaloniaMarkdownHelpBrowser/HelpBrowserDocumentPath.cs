using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HappyCoding.AvaloniaMarkdownHelpBrowser.Util;

namespace HappyCoding.AvaloniaMarkdownHelpBrowser
{
    public record HelpBrowserDocumentPath(
        Assembly HostAssembly,
        string EmbeddedResourceName) : IHelpBrowserDocumentPath
    {
        public string EmbeddedResourceDirectory
        {
            get => GetEmbeddedResourceDirectory(this.EmbeddedResourceName);
        }

        public static string GetEmbeddedResourceDirectory(string embeddedResourceName)
        {
            var directoryDotIndex = 0;
            var dotCounter = 0;
            for (var loop = embeddedResourceName.Length - 1; loop >= 0; loop--)
            {
                if (embeddedResourceName[loop] == '.') { dotCounter++; }
                if (dotCounter >= 2)
                {
                    directoryDotIndex = loop;
                    break;
                }
            }

            if (directoryDotIndex == 0) { return string.Empty; }
            return embeddedResourceName[..directoryDotIndex];
        }

        public static string GetEmbeddedResourceFileNameWithoutExtension(string embeddedResourceName)
        {
            var strBuilder = PooledStringBuilders.Current.TakeStringBuilder(64);
            try
            {
                var firstDotPassed = false;
                for (var loop = embeddedResourceName.Length - 1; loop >= 0; loop--)
                {
                    if (embeddedResourceName[loop] == '.')
                    {
                        if (!firstDotPassed)
                        {
                            firstDotPassed = true;
                            continue;
                        }
                        else { break; }
                    }

                    if (firstDotPassed)
                    {
                        strBuilder.Insert(0, embeddedResourceName[loop]);
                    }
                }
                return strBuilder.ToString();
            }
            finally
            {
                PooledStringBuilders.Current.ReRegisterStringBuilder(strBuilder);
            }
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return this.EmbeddedResourceName;
        }

        public TextReader OpenRead()
        {
            var inStream = this.HostAssembly.GetManifestResourceStream(this.EmbeddedResourceName);
            if (inStream == null)
            {
                throw new InvalidOperationException(
                    $"Resource {this.EmbeddedResourceName} not found in assembly {this.HostAssembly.FullName}");
            }

            return new StreamReader(inStream);
        }
    }
}
