using HappyCoding.ConsoleLogWindow.Application.Exceptions;
using HappyCoding.ConsoleLogWindow.Application.Model;
using HappyCoding.ConsoleLogWindow.Application.Ports;
using Newtonsoft.Json;

namespace HappyCoding.ConsoleLogWindow.JsonDocumentFileHandler;

internal class JsonDocumentFileHandlerAdapter : IDocumentFileHandler
{
    private readonly JsonSerializer _serializer;

    public JsonDocumentFileHandlerAdapter()
    {
        _serializer = new JsonSerializer();
        _serializer.Formatting = Formatting.Indented;
    }

    /// <inheritdoc />
    public async Task SaveDocumentToFileAsync(DocumentModel model, string fileName)
    {
        using var jsonWriter = new JsonTextWriter(
            new StreamWriter(
                new FileStream(fileName, FileMode.CreateNew, FileAccess.Write)));

        await Task.Run(() =>
        {
            _serializer.Serialize(jsonWriter, model);
        });
    }

    /// <inheritdoc />
    public async Task<DocumentModel> LoadDocumentFromFileAsync(DocumentModel model, string fileName)
    {
        using var jsonReader = new JsonTextReader(
            new StreamReader(
                new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read)));

        var result = await Task.Run(() => _serializer.Deserialize<DocumentModel>(jsonReader));
        if (result == null)
        {
            throw new ConsoleLogWindowAdapterException(
                nameof(JsonDocumentFileHandlerAdapter),
                $"Unable to deserialize {nameof(DocumentModel)} from file {fileName}");
        }

        return result;
    }
}
