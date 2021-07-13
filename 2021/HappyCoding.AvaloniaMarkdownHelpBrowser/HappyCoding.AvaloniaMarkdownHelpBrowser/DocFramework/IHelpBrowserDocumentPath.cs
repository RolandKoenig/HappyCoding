using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HappyCoding.AvaloniaMarkdownHelpBrowser.DocFramework
{
    public interface IHelpBrowserDocumentPath
    {
        Assembly HostAssembly { get; }

        string EmbeddedResourceDirectory { get; }

        HelpBrowserDocumentPath FollowLocalPath(string localFileSystemPath);

        TextReader OpenRead();
    }
}
