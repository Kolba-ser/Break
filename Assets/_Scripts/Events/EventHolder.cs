

using System;

public sealed class EventHolder : Singletone<EventHolder>, IDisposable
{
    private GlobalEvent onMenuOpenedEvent = new GlobalEvent();
    private GlobalEvent onMenuClosedEvent = new GlobalEvent();

    public GlobalEvent OnMenuOpenedEvent => onMenuOpenedEvent;
    public GlobalEvent OnMenuClosedEvent => onMenuClosedEvent;

    public void Dispose()
    {
        onMenuOpenedEvent.Dispose();
        onMenuClosedEvent.Dispose();
    }
}

