using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using HappyCoding.AvaloniaMarkdownHelpBrowser.DocFramework;
using HappyCoding.AvaloniaMarkdownHelpBrowser.Util;

namespace HappyCoding.AvaloniaMarkdownHelpBrowser
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private IntegratedDocRepository _docRepository;
        private HelpBrowserDocument? _selectedDocument;

        public IEnumerable<HelpBrowserDocument> AllDocuments => _docRepository.AllDocuments;

        public DelegateCommand<string> CommandNavigateTo { get; }

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

            this.CommandNavigateTo = new DelegateCommand<string>((navTarget) =>
            {
                if (navTarget == null) { return; }
                if (this.SelectedDocument == null) { return; }

                var newPath = this.SelectedDocument.DocumentPath.FollowLocalPath(navTarget);
                var targetDoc = _docRepository.TryGetByPath(newPath);
                if (targetDoc != null)
                {
                    this.SelectedDocument = targetDoc;
                }
            });
        }

        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
