using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HappyCoding.WpfWithMaterialDesign.Controls
{
    public partial class MainWindowBase : Window
    {
        public ICommand Command_Minimize { get; }

        public ICommand Command_Maximize { get; }

        public ICommand Command_Restore { get; }

        public ICommand Command_Close { get; }

        public MainWindowBase()
        {
            this.Command_Minimize = new MainWindowCommand(
                () => this.WindowState = WindowState.Minimized);
            this.Command_Maximize = new MainWindowCommand(
                () => this.WindowState = WindowState.Maximized,
                () => this.WindowState != WindowState.Maximized);
            this.Command_Restore = new MainWindowCommand(
                () => this.WindowState = WindowState.Normal,
                () => this.WindowState == WindowState.Maximized);
            this.Command_Close = new MainWindowCommand(this.Close);
        }
    }
}
