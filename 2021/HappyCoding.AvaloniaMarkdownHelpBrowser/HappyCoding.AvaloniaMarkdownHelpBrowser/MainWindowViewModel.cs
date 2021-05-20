using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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

        public string[] PossibleMarkDownNames { get; private set; }

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
            _selectedSelectedMarkDownContentContent = string.Empty;
            _selectedMarkDownName = "-";
            this.PossibleMarkDownNames = new[]
            {
                "-",
                "Test 1",
                "Test 2",
                "Test 3"
            };
        }

        private void UpdateMarkdownContent(string markDownName)
        {
            switch (markDownName)
            {
                case "":
                case "-":
                    this.SelectedMarkDownContent = string.Empty;
                    break;

                case "Test 1":
                    this.SelectedMarkDownContent = Properties.Resources.Test1;
                    break;

                case "Test 2":
                    this.SelectedMarkDownContent = Properties.Resources.Test2;
                    break;

                case "Test 3":
                    this.SelectedMarkDownContent = Properties.Resources.Test3;
                    break;
            }
        }

        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
