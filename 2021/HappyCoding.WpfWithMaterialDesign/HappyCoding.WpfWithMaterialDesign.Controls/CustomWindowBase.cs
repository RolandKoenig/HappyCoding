using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HappyCoding.WpfWithMaterialDesign.Controls
{
    public partial class CustomWindowBase : Window
    {
        public static readonly DependencyProperty CustomTitleBarHeightProperty;

        public ICommand Command_Minimize { get; }

        public ICommand Command_Maximize { get; }

        public ICommand Command_Restore { get; }

        public ICommand Command_Close { get; }

        public double CustomTitleBarHeight
        {
            get { return (double) GetValue(CustomTitleBarHeightProperty); }
            set { SetValue(CustomTitleBarHeightProperty, value); }
        }

        static CustomWindowBase()
        {
            CustomTitleBarHeightProperty = DependencyProperty.Register(
                "CustomTitleBarHeight", typeof(double), typeof(CustomWindowBase), new PropertyMetadata(32.0));
            ;

            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(CustomWindowBase), 
                new FrameworkPropertyMetadata(typeof(CustomWindowBase)));
        }

        public CustomWindowBase()
        {
            this.Command_Minimize = new CustomWindowCommand(
                () => this.WindowState = WindowState.Minimized);
            this.Command_Maximize = new CustomWindowCommand(
                () => this.WindowState = WindowState.Maximized,
                () => this.WindowState != WindowState.Maximized);
            this.Command_Restore = new CustomWindowCommand(
                () => this.WindowState = WindowState.Normal,
                () => this.WindowState == WindowState.Maximized);
            this.Command_Close = new CustomWindowCommand(this.Close);
        }

        protected override void OnStateChanged(EventArgs e)
        {
            base.OnStateChanged(e);

            ((CustomWindowCommand)this.Command_Maximize).RaiseCanExecuteChanged();
            ((CustomWindowCommand)this.Command_Restore).RaiseCanExecuteChanged();
        }
    }
}
