using System;

namespace HappyCoding.TemperatureViewer.Embedded;

public class DummyDisposable(Action disposeAction) : IDisposable
{
    public void Dispose()
    {
        disposeAction();
    }
}