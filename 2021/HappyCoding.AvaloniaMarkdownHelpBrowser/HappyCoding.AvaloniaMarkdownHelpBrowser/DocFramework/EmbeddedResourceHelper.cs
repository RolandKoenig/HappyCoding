using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HappyCoding.AvaloniaMarkdownHelpBrowser.Util;

namespace HappyCoding.AvaloniaMarkdownHelpBrowser.DocFramework
{
    public static class EmbeddedResourceHelper
    {
        public static string FollowLocalPath(
            string embeddedResourceDirectory,
            string localFileSystemPath)
        {
            var countDots = 0;
            for (var loop = 0; loop < embeddedResourceDirectory.Length; loop++)
            {
                if (embeddedResourceDirectory[loop] == '.') { countDots++; }
            }

            var directoryStack = new List<ReadOnlyMemory<char>>(countDots + 1);
            foreach (var actSplitPart in embeddedResourceDirectory.SplitZeroAlloc('.'))
            {
                directoryStack.Add(actSplitPart);
            }

            var strBuilder = PooledStringBuilders.Current.TakeStringBuilder(localFileSystemPath.Length);
            try
            {
                // Update the stack by given local path
                for (var loop = 0; loop < localFileSystemPath.Length; loop++)
                {
                    var actChar = localFileSystemPath[loop];
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
                                        $"Parameter {nameof(localFileSystemPath)} would point beneath the root path!");
                                }

                                directoryStack.RemoveAt(
                                    directoryStack.Count - 1);
                            }
                            else
                            {
                                directoryStack.Add(
                                    strBuilder.ToString().AsMemory());
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
                return strBuilder.ToString();
            }
            finally
            {
                PooledStringBuilders.Current.ReRegisterStringBuilder(strBuilder);
            }
        }
    }
}
