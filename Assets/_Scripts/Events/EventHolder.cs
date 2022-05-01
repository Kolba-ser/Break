

using System;

public sealed class EventHolder : Singletone<EventHolder>, IDisposable
{
    private GlobalEvent onMenuOpenedEvent = new GlobalEvent();
    private GlobalEvent onMenuClosedEvent = new GlobalEvent();
    private GlobalEvent<bool> onEndGame = new GlobalEvent<bool>();

    public GlobalEvent OnMenuOpenedEvent => onMenuOpenedEvent;
    public GlobalEvent OnMenuClosedEvent => onMenuClosedEvent;
    public GlobalEvent<bool> OnEndGame => onEndGame;

    public void Dispose()
    {
        onMenuOpenedEvent.Dispose();
        onMenuClosedEvent.Dispose();
        onEndGame.Dispose();
    }
}

