using System.Collections.ObjectModel;

namespace HappyCoding.ConsoleLogWindow.Application.Model;

public class DocumentModel
{
    public ObservableCollection<ProcessGroup> ProcessGroups { get; private set; } = new();

    public DocumentModel()
    {

    }
}
