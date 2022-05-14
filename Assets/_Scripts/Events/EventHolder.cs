using System;

public sealed class EventHolder : IDisposable
{
    private GlobalEvent onMenuOpenedEvent = new GlobalEvent();
    private GlobalEvent onMenuClosedEvent = new GlobalEvent();
    private GlobalEvent onLevelChanged = new GlobalEvent();
    private GlobalEvent<bool> onEndGame = new GlobalEvent<bool>();
    
    private GlobalEvent<int> onEnemyDie = new GlobalEvent<int>();


    public GlobalEvent OnMenuOpenedEvent => onMenuOpenedEvent;
    public GlobalEvent OnMenuClosedEvent => onMenuClosedEvent;
    public GlobalEvent OnLevelChanged => onLevelChanged;
    public GlobalEvent<bool> OnEndGame => onEndGame;
    public GlobalEvent<int> OnEnemyDie => onEnemyDie;


    void IDisposable.Dispose()
    {
        onMenuOpenedEvent.Dispose();
        onMenuClosedEvent.Dispose();
        onEndGame.Dispose();
        onEnemyDie.Dispose();
        onLevelChanged.Dispose();
    }
}

