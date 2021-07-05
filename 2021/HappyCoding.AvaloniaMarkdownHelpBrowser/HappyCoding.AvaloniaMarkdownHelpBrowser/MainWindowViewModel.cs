using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace HappyCoding.AvaloniaMarkdownHelpBrowser
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private string _selectedSelectedMarkDownContentContent;
        private string _selectedMarkDownName;
        private IntegratedDocRepository _docRepository;

        public string SelectedMarkDownContent
        {
            get => _selectedSelectedMarkDownContentContent;
            set
            {
                if (_selectedSelectedMarkDownContentContent != value)
                {
                    _selectedSelectedMarkDownContentContent = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        public IEnumerable<string> PossibleMarkDownNames =>
            _docRepository.AllFiles.Select(actFile => actFile.YamlHeader.Title);

        public string SelectedMarkDownName
        {
            get => _selectedMarkDownName;
            set
            {
                if (_selectedMarkDownName != value)
                {
                    _selectedMarkDownName = value;
                    this.RaisePropertyChanged();

                    this.UpdateMarkdownContent(value);
                }
            }
        }

        public MainWindowViewModel()
        {
            _docRepository = new IntegratedDocRepository(
                Assembly.GetExecutingAssembly());

            var firstDocument = _docRepository.AllFiles.FirstOrDefault();
            if (firstDocument != null)
            {
                _selectedSelectedMarkDownContentContent = firstDocument.MarkdownContentString;
                _selectedMarkDownName = firstDocument.YamlHeader.Title;
            }
            else
            {
                _selectedSelectedMarkDownContentContent = string.Empty;
                _selectedMarkDownName = string.Empty;
            }
        }

        private void UpdateMarkdownContent(string markDownName)
        {
            var selectedDocument = _docRepository.GetByTitle(markDownName);
            this.SelectedMarkDownContent = selectedDocument.MarkdownContentString;
            //switch (markDownName)
            //{
            //    case "":
            //    case "-":
            //        this.SelectedMarkDownContent = string.Empty;
            //        break;

            //    case "Test 1":
            //        this.SelectedMarkDownContent = Properties.Resources.Test1;
            //        break;

            //    case "Test 2":
            //        this.SelectedMarkDownContent = Properties.Resources.Test2;
            //        break;

            //    case "Test 3":
            //        this.SelectedMarkDownContent = Properties.Resources.Test3;
            //        break;
            //}
        }

        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
