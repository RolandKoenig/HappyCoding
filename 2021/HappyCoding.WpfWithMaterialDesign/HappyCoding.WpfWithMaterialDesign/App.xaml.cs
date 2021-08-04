using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using HappyCoding.WpfWithMaterialDesign.Util;

namespace HappyCoding.WpfWithMaterialDesign
{
    public partial class App : Application
    {
        private AppSkin _skin;

        public AppSkin Skin
        {
            get => _skin;
            set
            {
                if (_skin != value)
                {
                    _skin = value;

                    foreach (var actResourceDict in this.Resources.MergedDictionaries)
                    {
                        if (actResourceDict is SkinResourceDictionary skinDict)
                        {
                            skinDict.UpdateSource();
                        }
                        else if(actResourceDict.Source != null)
                        {
                            actResourceDict.Source = actResourceDict.Source;
                        }
                    }
                }
            }
        }

        public static App CurrentApp => (App)Current;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            this.Skin = AppSkin.MaterialLight;
        }
    }
}
