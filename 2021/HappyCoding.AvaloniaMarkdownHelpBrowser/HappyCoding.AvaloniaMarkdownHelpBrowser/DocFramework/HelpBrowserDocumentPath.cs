using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HappyCoding.AvaloniaMarkdownHelpBrowser.Util;

namespace HappyCoding.AvaloniaMarkdownHelpBrowser.DocFramework
{
    public record HelpBrowserDocumentPath(
        Assembly HostAssembly,
        string EmbeddedResourceName) : IHelpBrowserDocumentPath
    {
        public string EmbeddedResourceDirectory
        {
            get => GetEmbeddedResourceDirectory(this.EmbeddedResourceName);
        }

        /// <summary>
        /// Follows a local path starting from this resource.
        /// The local path may contain /, \ and ..
        /// </summary>
        public HelpBrowserDocumentPath FollowLocalPath(string localPath)
        {
            // Create a stack based on current directory
            var currentDirectoryPath = this.EmbeddedResourceDirectory.Split('.', StringSplitOptions.RemoveEmptyEntries);
            var directoryStack = new List<string>(currentDirectoryPath);

            var strBuilder = PooledStringBuilders.Current.TakeStringBuilder(localPath.Length);
            try
            {
                // Update the stack by given local path
                for (var loop = 0; loop < localPath.Length; loop++)
                {
                    var actChar = localPath[loop];
                    if ((actChar == Path.DirectorySeparatorChar) ||
                        (actChar == Path.AltDirectorySeparatorChar))
                    {
                        if (strBuilder.Length > 0)
                        {
                            if(strBuilder.Equals(".")){ }
                            else if (strBuilder.Equals(".."))
                            {
                                if (directoryStack.Count == 0)
                                {
                                    throw new ArgumentException(
                                        $"Parameter {nameof(localPath)} would point beneath the root path!");
                                }

                                directoryStack.RemoveAt(
                                    directoryStack.Count - 1);
                            }
                            else
                            {
                                directoryStack.Add(strBuilder.ToString());
                            }
                        }
                        strBuilder.Clear();
                    }
                    else
                    {
                        strBuilder.Append(actChar);
                    }
                }

                // Last content of StringBuilder is the filename
                var newFileName = strBuilder.ToString();

                // Build directory path
                strBuilder.Clear();
                foreach (var actDirectoryPart in directoryStack)
                {
                    if(actDirectoryPart.Length == 0) { continue; }

                    if (strBuilder.Length > 0) { strBuilder.Append('.'); }
                    strBuilder.Append(actDirectoryPart);
                }
                if (strBuilder.Length > 0) { strBuilder.Append('.'); }
                strBuilder.Append(newFileName);

                // Build result object
                return new HelpBrowserDocumentPath(
                    this.HostAssembly,
                    strBuilder.ToString());
            }
            finally
            {
                PooledStringBuilders.Current.ReRegisterStringBuilder(strBuilder);
            }
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
            var lastDotIndex = -1;
            var nextToLastDotIndex = -1;
            for (var loop = embeddedResourceName.Length - 1; loop >= 0; loop--)
            {
                if (embeddedResourceName[loop] != '.') { continue; }

                if (lastDotIndex == -1)
                {
                    lastDotIndex = loop;
                }
                else
                {
                    nextToLastDotIndex = loop;
                    break;
                }
            }

            if (nextToLastDotIndex > -1)
            {
                if (lastDotIndex == (nextToLastDotIndex + 1)) { return string.Empty; }
                return embeddedResourceName.Substring(
                    nextToLastDotIndex + 1, 
                    (lastDotIndex - nextToLastDotIndex) - 1);
            }
            else if (lastDotIndex > -1)
            {
                return embeddedResourceName[..lastDotIndex];
            }
            else
            {
                return embeddedResourceName;
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
