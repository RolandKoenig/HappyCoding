using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HappyCoding.WpfWithMaterialDesign.Data;
using HappyCoding.WpfWithMaterialDesign.Util;

namespace HappyCoding.WpfWithMaterialDesign
{
    public class MainWindowViewModel : PropertyChangedBase
    {
        private TestDataRow? _selectedTestDataRow;

        public ObservableCollection<TestDataRow> TestData { get; }

        public TestDataRow? SelectedTestDataRow
        {
            get => _selectedTestDataRow;
            set
            {
                if (_selectedTestDataRow != value)
                {
                    _selectedTestDataRow = value;
                    this.RaisePropertyChanged(nameof(this.SelectedTestDataRow));
                }
            }
        }

        public MainWindowViewModel(int testDataRows)
        {
            this.TestData = new ObservableCollection<TestDataRow>(
                TestDataRow.CreateTestData(testDataRows));
            if (this.TestData.Count > 0)
            {
                this.SelectedTestDataRow = this.TestData[0];
            }
        }
    }
}
