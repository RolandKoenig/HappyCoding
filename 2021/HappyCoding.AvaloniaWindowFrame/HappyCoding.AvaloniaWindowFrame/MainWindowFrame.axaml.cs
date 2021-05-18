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
        private StackPanel _ctrlTitlePanel;
        private Panel _ctrlHeaderContent;
        private Panel _ctrlMainContent;
        private Panel _ctrlFooterContent;

        public Controls CustomTitleContent => _ctrlTitlePanel.Children;
        public Controls HeaderContent => _ctrlHeaderContent.Children;
        public Controls MainContent => _ctrlMainContent.Children;
        public Controls FooterContent => _ctrlFooterContent.Children;

        public bool TryExtendClientAreaToDecoration { get; set; } = true;

        public MainWindowFrame()
        {
            AvaloniaXamlLoader.Load(this);

            _ctrlMainGrid = this.Find<Grid>("CtrlMainGrid");
            _ctrlTitlePanel = this.Find<StackPanel>("CtrlTitlePanel");
            _ctrlHeaderContent = this.Find<Panel>("CtrlHeader");
            _ctrlMainContent = this.Find<Panel>("CtrlMainContent");
            _ctrlFooterContent = this.Find<Panel>("CtrlFooter");
        }

        private void TryConfigureParentWindow()
        {
            if (_mainWindow == null) { return; }

            _mainWindow.ExtendClientAreaToDecorationsHint = this.TryExtendClientAreaToDecoration;
            _mainWindow.ExtendClientAreaTitleBarHeightHint = -1;
            _mainWindow.TransparencyLevelHint = WindowTransparencyLevel.None;
        }

        private void UpdateMainWindowFrameState()
        {
            if (_mainWindow == null) { return; }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                _ctrlTitlePanel.Margin = new Thickness(0.0, 0.0);
                _ctrlTitlePanel.HorizontalAlignment = HorizontalAlignment.Center;
                _ctrlTitlePanel.VerticalAlignment = VerticalAlignment.Center;
            }
            else
            {
                _ctrlTitlePanel.Margin = new Thickness(FULL_SCREEN_WINDOW_PADDING, 0.0);
                _ctrlTitlePanel.HorizontalAlignment = HorizontalAlignment.Left;
                _ctrlTitlePanel.VerticalAlignment = VerticalAlignment.Center;
            }

            switch (_mainWindow.WindowState)
            {
                case WindowState.Maximized:
                case WindowState.FullScreen:
                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
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
                _ctrlTitlePanel.IsVisible = true;
            }
            else
            {
                _ctrlTitlePanel.IsVisible = false;
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

                    // Call TryConfigureParentWindow in a separate ui thread pass to insure that local properties are set by xaml
                    Dispatcher.UIThread.Post(this.TryConfigureParentWindow);
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
