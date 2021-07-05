using System;
using System.Collections.Generic;
using System.IO;
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

                using var resourceStream = source.GetManifestResourceStream(actEmbeddedRes);
                if (resourceStream == null)
                {
                    throw new ApplicationException(
                        $"Unable to open stream to file {actEmbeddedRes} from assembly {source.FullName}!");
                }
                using var textReader = new StreamReader(resourceStream);

                var docFile = new HelpBrowserDocument(actEmbeddedRes, textReader);
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
