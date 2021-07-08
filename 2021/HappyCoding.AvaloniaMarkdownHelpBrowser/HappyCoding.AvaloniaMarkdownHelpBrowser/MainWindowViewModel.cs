using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using HappyCoding.AvaloniaMarkdownHelpBrowser.DocFramework;

namespace HappyCoding.AvaloniaMarkdownHelpBrowser
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private IntegratedDocRepository _docRepository;
        private HelpBrowserDocument? _selectedDocument;

        public IEnumerable<HelpBrowserDocument> AllDocuments => _docRepository.AllDocuments;

        public HelpBrowserDocument? SelectedDocument
        {
            get => _selectedDocument;
            set
            {
                if (_selectedDocument != value)
                {
                    _selectedDocument = value;
                    this.RaisePropertyChanged(nameof(this.SelectedDocument));
                }
            }
        }

        public MainWindowViewModel()
        {
            _docRepository = new IntegratedDocRepository(
                Assembly.GetExecutingAssembly());
        }

        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
