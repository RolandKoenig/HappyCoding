using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HappyCoding.WpfWithMaterialDesign.Controls;

namespace HappyCoding.WpfWithMaterialDesign
{
    public partial class MainWindow : CustomWindowBase
    {
        public MainWindow()
        {
            InitializeComponent();

            this.Loaded += this.OnMainWindow_Loaded;
        }

        private void OnMainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(this)) { return; }

            this.DataContext = new MainWindowViewModel(10000);
        }

        private void OnMenuThemeDefault_Click(object sender, RoutedEventArgs e)
        {
            ((App) Application.Current).SwitchThemeTo(AppTheme.Default);
        }

        private void OnMenuThemeDark_Click(object sender, RoutedEventArgs e)
        {
            ((App) Application.Current).SwitchThemeTo(AppTheme.MaterialDark);
        }

        private void OnMenuThemeLight_Click(object sender, RoutedEventArgs e)
        {
            ((App)Application.Current).SwitchThemeTo(AppTheme.MaterialLight);
        }

        private void OnMenuExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
