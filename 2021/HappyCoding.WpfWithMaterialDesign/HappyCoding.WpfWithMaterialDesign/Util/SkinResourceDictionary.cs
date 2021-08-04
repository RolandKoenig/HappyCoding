using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HappyCoding.WpfWithMaterialDesign.Util
{
    // A resource dictionary which can choose between skins
    // see https://michaelscodingspot.com/wpf-complete-guide-themes-skins/

    public class SkinResourceDictionary : ResourceDictionary
    {
        private Uri _defaultSource;
        private Uri _darkSource;
        private Uri _lightSource;
 
        public Uri DefaultSource
        {
            get => _defaultSource;
            set {
                _defaultSource = value;
                UpdateSource();
            }
        }

        public Uri DarkSource
        {
            get => _darkSource;
            set {
                _darkSource = value;
                UpdateSource();
            }
        }
 
        public Uri LightSource
        {
            get => _lightSource;
            set {
                _lightSource = value;
                UpdateSource();
            }
        }

        public void UpdateSource()
        {
            switch (App.CurrentApp.Skin)
            {
                case AppSkin.Default:
                    this.Source = _defaultSource;
                    break;

                case AppSkin.MaterialDark:
                    this.Source = _darkSource;
                    break;

                case AppSkin.MaterialLight:
                    this.Source = _lightSource;
                    break;
            }
        }
    }
}
