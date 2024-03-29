﻿using Microsoft.UI.Xaml;
using SeeingSharp;
using SeeingSharp.Core;

namespace HappyCoding.SeeingSharp2WithPicking
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        private Window? _window;

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            // Initialize graphics
            GraphicsCore.Loader
                .SupportWinUI()
                .Load();

            _window = new MainWindow();
            _window.Activate();
        }
    }
}
