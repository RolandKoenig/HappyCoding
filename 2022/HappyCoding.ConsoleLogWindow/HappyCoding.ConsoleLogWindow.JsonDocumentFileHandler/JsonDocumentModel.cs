using System.Reflection;
using HappyCoding.ConsoleLogWindow.Application.Model;
using Newtonsoft.Json;

namespace HappyCoding.ConsoleLogWindow.JsonDocumentFileHandler;

internal class JsonDocumentModel
{
    [JsonProperty]
    public string ToolName => nameof(ConsoleLogWindow);

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string? ToolVersion => Assembly.GetExecutingAssembly().GetName().Version?.ToString();

    [JsonProperty]
    public List<ProcessGroup> ProcessGroups { get; } = new();
}
