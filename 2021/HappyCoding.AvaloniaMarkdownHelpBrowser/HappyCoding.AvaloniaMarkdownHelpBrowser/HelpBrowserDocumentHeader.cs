using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyCoding.AvaloniaMarkdownHelpBrowser
{
    public class HelpBrowserDocumentHeader
    {
        public static readonly HelpBrowserDocumentHeader Empty = new HelpBrowserDocumentHeader();

        public string Title { get; set; } = string.Empty;

        public string Author { get; set; } = string.Empty;
    }
}
