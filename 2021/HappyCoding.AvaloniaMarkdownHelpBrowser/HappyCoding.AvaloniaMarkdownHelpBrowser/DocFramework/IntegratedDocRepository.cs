using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HappyCoding.AvaloniaMarkdownHelpBrowser.DocFramework
{
    public class IntegratedDocRepository
    {
        private List<HelpBrowserDocument> _lstAllDocuments;
        private Dictionary<IHelpBrowserDocumentPath, HelpBrowserDocument> _dictAllFiles;

        public IEnumerable<HelpBrowserDocument> AllDocuments => _lstAllDocuments;

        public IntegratedDocRepository()
        {
            _lstAllDocuments = new List<HelpBrowserDocument>(16);
            _dictAllFiles = new Dictionary<IHelpBrowserDocumentPath, HelpBrowserDocument>(16);
        }

        public IntegratedDocRepository(Assembly source)
            : this()
        {
            this.AddAssembly(source);
        }

        public void AddAssembly(Assembly assembly)
        {
            var manifestResourceNames = assembly.GetManifestResourceNames();

            var countMarkdowns = 0;
            foreach (var actEmbeddedRes in manifestResourceNames)
            {
                if (!actEmbeddedRes.EndsWith(".md", StringComparison.OrdinalIgnoreCase)) { continue; }
                countMarkdowns++;
            }
            if (countMarkdowns == 0) { return; }

            var foundDocuments = new List<HelpBrowserDocument>(countMarkdowns);
            foreach (var actEmbeddedRes in manifestResourceNames)
            {
                if(!actEmbeddedRes.EndsWith(".md", StringComparison.OrdinalIgnoreCase)){ continue; }

                var documentPath = new HelpBrowserDocumentPath(assembly, actEmbeddedRes);
                var docFile = new HelpBrowserDocument(documentPath);
                if (docFile.IsValid)
                {
                    if (_dictAllFiles.ContainsKey(docFile.DocumentPath))
                    {
                        throw new ApplicationException($"Duplicate documentation file: {docFile.DocumentPath}!");
                    }
                    
                    foundDocuments.Add(docFile);
                    _dictAllFiles.Add(docFile.DocumentPath, docFile);
                }
            }
            foundDocuments.Sort(
                (left, right) => string.Compare(left.Title, right.Title, StringComparison.Ordinal));
            
            _lstAllDocuments.AddRange(foundDocuments);
        }

        public HelpBrowserDocument GetByPath(IHelpBrowserDocumentPath path)
        {
            var requestedDocument = this.TryGetByPath(path);
            if (requestedDocument == null)
            {
                throw new FileNotFoundException(
                    $"Unable to find documentation file by title '{path}'", 
                    path.ToString());
            }
            return requestedDocument;
        }

        public HelpBrowserDocument? TryGetByPath(IHelpBrowserDocumentPath path)
        {
            if (_dictAllFiles.TryGetValue(path, out var foundFile))
            {
                return foundFile;
            }
            return null;
        }
    }
}
