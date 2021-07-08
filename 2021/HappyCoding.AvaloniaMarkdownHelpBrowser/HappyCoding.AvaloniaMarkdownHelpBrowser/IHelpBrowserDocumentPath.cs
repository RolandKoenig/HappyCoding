using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HappyCoding.AvaloniaMarkdownHelpBrowser
{
    public interface IHelpBrowserDocumentPath
    {
        Assembly HostAssembly { get; }

        string EmbeddedResourceDirectory { get; }

        TextReader OpenRead();
    }
}
