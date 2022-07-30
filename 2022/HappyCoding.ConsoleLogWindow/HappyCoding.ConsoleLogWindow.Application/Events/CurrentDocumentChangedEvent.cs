using HappyCoding.ConsoleLogWindow.Application.Model;
using HappyCoding.ConsoleLogWindow.Messenger;

namespace HappyCoding.ConsoleLogWindow.Application.Events;

[FirLibMessage]
public record CurrentDocumentChangedEvent(DocumentModel NewDocument);