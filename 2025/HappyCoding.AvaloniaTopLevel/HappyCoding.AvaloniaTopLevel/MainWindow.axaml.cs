using System;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Logging;
using Avalonia.Platform.Storage;

namespace HappyCoding.AvaloniaTopLevel;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private async void OnCmdCopy_Click(object? sender, RoutedEventArgs e)
    {
        try
        {
            // Window is actually a TopLevel
            // To be more generic, get it by TopLevel.GetTopLevel(Visual)
            var topLevel = TopLevel.GetTopLevel(this.TxtTextForClipBoard);
            if (topLevel?.Clipboard == null){ return; }
            
            // Put text into the platform's Clipboard
            await topLevel.Clipboard.SetTextAsync(this.TxtTextForClipBoard.Text ?? string.Empty);
        }
        catch (Exception ex)
        {
            // Log using the Avalonia logging mechanism
            Logger.TryGet(LogEventLevel.Error, "Custom")
                ?.Log(this, "Unable to copy text to clipboard: {Exception}", ex);
        }
    }

    private async void OnCmdPaste_Click(object? sender, RoutedEventArgs e)
    {
        try
        {
            // Window is actually a TopLevel
            // To be more generic, get it by TopLevel.GetTopLevel(Visual)
            var topLevel = TopLevel.GetTopLevel(this.TxtTextForClipBoard);
            if (topLevel?.Clipboard == null){ return; }
            
            // Put text into the platform's Clipboard
            this.TxtTextForClipBoard.Text = await topLevel.Clipboard.GetTextAsync();
        }
        catch (Exception ex)
        {
            // Log using the Avalonia logging mechanism
            Logger.TryGet(LogEventLevel.Error, "Custom")
                ?.Log(this, "Unable to text text from clipboard: {Exception}", ex);
        }
    }

    private async void OnCmdSelectFile_Click(object? sender, RoutedEventArgs e)
    {
        try
        {
            // Window is actually a TopLevel
            // To be more generic, get it by TopLevel.GetTopLevel(Visual)
            var topLevel = TopLevel.GetTopLevel(this.TxtTextForClipBoard);
            if (topLevel?.Clipboard == null)
            {
                return;
            }

            // Select a file
            var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions());
            var selectedFile = files.FirstOrDefault();
            this.TextSelectedFile.Text = selectedFile?.Name ?? string.Empty;
        }
        catch (Exception ex)
        {
            // Log using the Avalonia logging mechanism
            Logger.TryGet(LogEventLevel.Error, "Custom")
                ?.Log(this, "Unable to pick a file using FilePicker: {Exception}", ex);
        }
    }
}