using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

namespace HappyCoding.WpfWithLiveCharts
{
    public class MainWindowViewModel
    {
        public SeriesCollection SeriesCollection { get; }

        public Func<double, string> LabelKmFormatter => (val) => val.ToString("N1");

        public Func<double, string> LabelMFormatter => (val) => val.ToString("N0");

        public MainWindowViewModel()
        {
            this.SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(1.0, 550.0),
                        new ObservablePoint(2.0, 600.0),
                        new ObservablePoint(5.0, 700.0),
                        new ObservablePoint(6.0, 1100.0),
                        new ObservablePoint(10.0, 1000.0),
                        new ObservablePoint(10.5, 700.0),
                        new ObservablePoint(11.0, 550.0),
                    }
                }
            };
        }
    }
}
