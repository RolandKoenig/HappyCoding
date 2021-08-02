using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace HappyCoding.WpfWithMaterialDesign
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            this.SwitchThemeTo(AppTheme.MaterialLight);
        }

        public void SwitchThemeTo(AppTheme targetTheme)
        {
            var emptyResDict = new ResourceDictionary();

            ResourceDictionary? resDictTheme = null;
            switch (targetTheme)
            {
                case AppTheme.MaterialDark:
                    resDictTheme = this.FindResource("ResThemeMaterialDark") as ResourceDictionary;
                    break;

                case AppTheme.MaterialLight:
                    resDictTheme = this.FindResource("ResThemeMaterialLight") as ResourceDictionary;
                    break;

                default:
                    resDictTheme = this.FindResource("ResThemeDefault") as ResourceDictionary;
                    break;
            }

            if (resDictTheme != null)
            {
                this.Resources.MergedDictionaries[1] = emptyResDict;
                this.Resources.MergedDictionaries[1] = resDictTheme;

                this.Resources.MergedDictionaries[1] = emptyResDict;
                this.Resources.MergedDictionaries[1] = resDictTheme;
            }
        }
    }
}
