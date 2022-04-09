

using System;

public sealed class EventHolder : Singletone<EventHolder>, IDisposable
{
    private GlobalEvent onDialogueStartEvent = new GlobalEvent();

    public GlobalEvent OnDialogueStartEvent => onDialogueStartEvent;

    public void Dispose()
    {
        onDialogueStartEvent.Dispose();
    }
}

