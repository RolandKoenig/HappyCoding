using System;
using HappyCoding.SimpleWinUI3App.Util;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using UnhandledExceptionEventArgs = Microsoft.UI.Xaml.UnhandledExceptionEventArgs;

namespace HappyCoding.SimpleWinUI3App
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        private MainWindow? _window;

        public IServiceProvider Services
        {
            get;
            private set;
        }

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Setup a ViewModel
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        /// <param name="page">the view</param>
        /// <returns>Initialized ViewModel</returns>
        public static TViewModel SetupViewModel<TViewModel>(Page page) 
            where TViewModel : ViewModelBase
        {
            var viewModel = ActivatorUtilities.CreateInstance<TViewModel>(((App) Current).Services);
            if (viewModel == null)
            {
                throw new InvalidOperationException(
                    $"Unable to create instance of service {typeof(TViewModel).FullName}!");
            }

            _ = new ViewModelProxy(viewModel, page);
            
            return viewModel;
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            _window = new MainWindow();

            this.Services = Startup.ConfigureServices(_window);
            
            _window.Activate();

            this.UnhandledException += this.OnUnhandledException;
        }

        private void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (_window == null)
            {
                return;
            }

            e.Handled = true;
            _window?.ShowUnhandledException(e.Exception);
        }
    }
}
