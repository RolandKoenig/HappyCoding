using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HappyCoding.WpfWithMaterialDesign.Controls
{
    /// <summary>
    /// 
    /// </summary>
    [ContentProperty(nameof(MainWindowMainPanel.Content))]
    public class MainWindowMainPanel : Control
    {
        public static readonly DependencyProperty ContentProperty;

        public object Content
        {
            get { return (object) GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        static MainWindowMainPanel()
        {
            ContentProperty = DependencyProperty.Register(
                "Content", typeof(object), typeof(MainWindowMainPanel), new PropertyMetadata(default(object)));

            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(MainWindowMainPanel), 
                new FrameworkPropertyMetadata(typeof(MainWindowMainPanel)));
        }

        public MainWindowMainPanel()
        {

        }
    }
}
