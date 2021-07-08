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
        private List<HelpBrowserDocument> _lstAllDocuments;
        private Dictionary<IHelpBrowserDocumentPath, HelpBrowserDocument> _dictAllFiles;

        public IEnumerable<HelpBrowserDocument> AllDocuments => _lstAllDocuments;

        public IntegratedDocRepository(Assembly source)
        {
            _lstAllDocuments = new List<HelpBrowserDocument>(16);
            _dictAllFiles = new Dictionary<IHelpBrowserDocumentPath, HelpBrowserDocument>(16);
            
            foreach (var actEmbeddedRes in source.GetManifestResourceNames())
            {
                if(!actEmbeddedRes.EndsWith(".md", StringComparison.OrdinalIgnoreCase)){ continue; }

                var documentPath = new HelpBrowserDocumentPath(source, actEmbeddedRes);
                var docFile = new HelpBrowserDocument(documentPath);
                if (docFile.IsValid)
                {
                    if (_dictAllFiles.ContainsKey(docFile.DocumentPath))
                    {
                        throw new ApplicationException($"Duplicate documentation file: {docFile.DocumentPath}!");
                    }
                    
                    _lstAllDocuments.Add(docFile);
                    _dictAllFiles.Add(docFile.DocumentPath, docFile);
                }
            }

            _lstAllDocuments.Sort(
                (left, right) => string.Compare(left.Title, right.Title, StringComparison.Ordinal));
        }

        //public HelpBrowserDocument GetByPath(HelpBrowserDocumentPath path)
        //{
        //    if (_dictAllFiles.TryGetValue(path, out var foundFile))
        //    {
        //        return foundFile;
        //    }
        //    throw new FileNotFoundException($"Unable to find documentation file by title '{title}'", title);
        //}
    }
}
