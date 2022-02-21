using HappyCoding.AvaloniaLogViewer.Domain.Model;

namespace HappyCoding.AvaloniaLogViewer.Domain;

public interface ILogFileLoader
{
    /// <summary>
    /// Gets the name of this loader.
    /// </summary>
    string LoaderName { get; }
    
    /// <summary>
    /// Gets all file extensions supported by this loader.
    /// </summary>
    IEnumerable<string> FileExtensions { get; }
    
    /// <summary>
    /// Loads all entries from the given file.
    /// </summary>
    /// <param name="inputFile"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    IAsyncEnumerable<LogFileEntry> LoadFileAsync(string inputFile, CancellationToken cancellationToken);
}