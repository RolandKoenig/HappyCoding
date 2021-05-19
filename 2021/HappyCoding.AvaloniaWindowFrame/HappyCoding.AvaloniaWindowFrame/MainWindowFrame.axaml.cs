using System.Runtime.InteropServices;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.LogicalTree;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;

namespace HappyCoding.AvaloniaWindowFrame
{
    public class MainWindowFrame : UserControl
    {
        private const double FULL_SCREEN_WINDOW_PADDING = 7.0;

        private Window? _mainWindow;
        private Grid _ctrlMainGrid;
        private StackPanel _ctrlCustomTitleArea;
        private Panel _ctrlHeaderArea;
        private Panel _ctrlMainContentArea;
        private Panel _ctrlFooterArea;

        public Controls CustomTitleArea => _ctrlCustomTitleArea.Children;
        public Controls HeaderArea => _ctrlHeaderArea.Children;
        public Controls MainContentArea => _ctrlMainContentArea.Children;
        public Controls FooterArea => _ctrlFooterArea.Children;

        public MainWindowFrame()
        {
            AvaloniaXamlLoader.Load(this);
        
            _ctrlMainGrid = this.Find<Grid>("CtrlMainGrid");
            _ctrlCustomTitleArea = this.Find<StackPanel>("CtrlCustomTitleArea");
            _ctrlHeaderArea = this.Find<Panel>("CtrlHeaderArea");
            _ctrlMainContentArea = this.Find<Panel>("CtrlMainContentArea");
            _ctrlFooterArea = this.Find<Panel>("CtrlFooterArea");
        }

        private void UpdateMainWindowFrameState()
        {
            if (_mainWindow == null) { return; }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                _ctrlCustomTitleArea.Margin = new Thickness(0.0, 0.0);
                _ctrlCustomTitleArea.HorizontalAlignment = HorizontalAlignment.Center;
                _ctrlCustomTitleArea.VerticalAlignment = VerticalAlignment.Center;
            }
            else
            {
                _ctrlCustomTitleArea.Margin = new Thickness(FULL_SCREEN_WINDOW_PADDING, 0.0);
                _ctrlCustomTitleArea.HorizontalAlignment = HorizontalAlignment.Left;
                _ctrlCustomTitleArea.VerticalAlignment = VerticalAlignment.Center;
            }

            switch (_mainWindow.WindowState)
            {
                case WindowState.Maximized:
                case WindowState.FullScreen:
                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) &&
                        _mainWindow.IsExtendedIntoWindowDecorations)
                    {
                        _ctrlMainGrid.Margin = new Thickness(FULL_SCREEN_WINDOW_PADDING);
                    }
                    break;

                default:
                    _ctrlMainGrid.Margin = new Thickness(0.0);
                    break;
            }

            if (_mainWindow.IsExtendedIntoWindowDecorations || (_mainWindow.WindowState == WindowState.FullScreen))
            {
                _ctrlMainGrid.RowDefinitions[0].Height = new GridLength(30.0);
                _ctrlCustomTitleArea.IsVisible = true;
            }
            else
            {
                _ctrlCustomTitleArea.IsVisible = false;
                _ctrlMainGrid.RowDefinitions[0].Height = new GridLength(0.0);
            }
        }

        /// <inheritdoc />
        protected override void OnAttachedToLogicalTree(LogicalTreeAttachmentEventArgs e)
        {
            base.OnAttachedToLogicalTree(e);

            var newMainWindow = e.Parent as Window;
            if (!ReferenceEquals(newMainWindow, _mainWindow))
            {
                if (_mainWindow != null)
                {
                    _mainWindow.PropertyChanged -= this.OnMainWindow_PropertyChanged;
                }
                _mainWindow = newMainWindow;
                if (_mainWindow != null)
                {
                    _mainWindow.PropertyChanged += this.OnMainWindow_PropertyChanged;
                }
            }

            // Trigger update of this control's state in a separate ui thread pass
            Dispatcher.UIThread.Post(this.UpdateMainWindowFrameState);
        }

        private void OnMainWindow_PropertyChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
        {
            this.UpdateMainWindowFrameState();
        }
    }
}
