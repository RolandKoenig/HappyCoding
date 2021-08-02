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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HappyCoding.WpfWithMaterialDesign.Controls
{
    /// <summary>
    /// </summary>
    public class MainWindowTitleBar : Control
    {
        private Window? _parentWindow;

        static MainWindowTitleBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(MainWindowTitleBar), 
                new FrameworkPropertyMetadata(typeof(MainWindowTitleBar)));
        }

        public MainWindowTitleBar()
        {
            _parentWindow = null;
        }

        private Window? FindWindow(DependencyObject currentElement)
        {
            var actParent = currentElement;
            while (actParent != null)
            {
                if (actParent is Window window) { return window; }

                actParent = LogicalTreeHelper.GetParent(actParent);
            }
            return null;
        }

        private void UpdateControlState()
        {
            if (_parentWindow == null) { return; }

            //if (_parentWindow.WindowState == WindowState.Maximized)
            //{
            //    this.CmdRestore.Visibility = Visibility.Visible;
            //    this.CmdMaximize.Visibility = Visibility.Collapsed;
            //}
            //else
            //{
            //    this.CmdRestore.Visibility = Visibility.Collapsed;
            //    this.CmdMaximize.Visibility = Visibility.Visible;
            //}
        }

        /// <inheritdoc />
        protected override void OnVisualParentChanged(DependencyObject oldParent)
        {
            base.OnVisualParentChanged(oldParent);

            if (_parentWindow != null)
            {
                _parentWindow.StateChanged -= this.OnParentWindow_StateChanged;
            }
            _parentWindow = this.FindWindow(this);
            if (_parentWindow != null)
            {
                _parentWindow.StateChanged += this.OnParentWindow_StateChanged;
            }

            this.UpdateControlState();
        }

        private void OnParentWindow_StateChanged(object? sender, EventArgs e)
        {
            this.UpdateControlState();
        }

        private void OnTitleBarMinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            if (_parentWindow != null)
            {
                _parentWindow.WindowState = WindowState.Minimized;
            }
        }

        private void OnTitleBarMaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            if (_parentWindow != null)
            {
                _parentWindow.WindowState = WindowState.Maximized;
            }
        }

        private void OnTitleBarRestoreButton_Click(object sender, RoutedEventArgs e)
        {
            if (_parentWindow != null)
            {
                _parentWindow.WindowState = WindowState.Normal;
            }
        }

        private void OnTitleBarCloseButton_Click(object sender, RoutedEventArgs e)
        {
            _parentWindow?.Close();
        }
    }
}
