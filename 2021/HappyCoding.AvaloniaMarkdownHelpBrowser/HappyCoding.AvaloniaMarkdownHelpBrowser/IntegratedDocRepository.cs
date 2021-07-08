using System;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HappyCoding.AvaloniaMarkdownHelpBrowser
{
    public class IntegratedDocRepository
    {
        private List<HelpBrowserDocument> _lstAllFiles;
        private Dictionary<string, HelpBrowserDocument> _dictAllFiles;

        public IEnumerable<HelpBrowserDocument> AllFiles => _lstAllFiles;

        public IntegratedDocRepository(Assembly source)
        {
            _lstAllFiles = new List<HelpBrowserDocument>(16);
            _dictAllFiles = new Dictionary<string, HelpBrowserDocument>(16);
            
            foreach (var actEmbeddedRes in source.GetManifestResourceNames())
            {
                if(!actEmbeddedRes.EndsWith(".md", StringComparison.OrdinalIgnoreCase)){ continue; }

                var documentPath = new HelpBrowserDocumentPath(source, actEmbeddedRes);
                var docFile = new HelpBrowserDocument(documentPath);
                if (docFile.IsValid)
                {
                    if (_dictAllFiles.ContainsKey(docFile.YamlHeader.Title))
                    {
                        throw new ApplicationException($"Duplicate documentation file title {docFile.YamlHeader.Title}!");
                    }
                    
                    _lstAllFiles.Add(docFile);
                    _dictAllFiles.Add(docFile.YamlHeader.Title, docFile);
                }
            }

            _lstAllFiles.Sort(
                (left, right) => string.Compare(left.Title, right.Title, StringComparison.Ordinal));
        }

        public HelpBrowserDocument GetByTitle(string title)
        {
            if (_dictAllFiles.TryGetValue(title, out var foundFile))
            {
                return foundFile;
            }
            throw new FileNotFoundException($"Unable to find documentation file by title '{title}'", title);
        }
    }
}
