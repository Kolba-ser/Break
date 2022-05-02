﻿

using System;
using UnityEngine;

public sealed class EventHolder : Singletone<EventHolder>, IDisposeHandler
{
    private GlobalEvent onMenuOpenedEvent = new GlobalEvent();
    private GlobalEvent onMenuClosedEvent = new GlobalEvent();
    private GlobalEvent onLevelChanged = new GlobalEvent();
    private GlobalEvent<bool> onEndGame = new GlobalEvent<bool>();
    // Костыль
    private GlobalEvent<int> onEnemyDie = new GlobalEvent<int>();


    public GlobalEvent OnMenuOpenedEvent => onMenuOpenedEvent;
    public GlobalEvent OnMenuClosedEvent => onMenuClosedEvent;
    public GlobalEvent OnLevelChanged => onLevelChanged;
    public GlobalEvent<bool> OnEndGame => onEndGame;
    public GlobalEvent<int> OnEnemyDie => onEnemyDie;


    public override void Dispose()
    {
        onMenuOpenedEvent.Dispose();
        onMenuClosedEvent.Dispose();
        onEndGame.Dispose();
        OnLevelChanged.Dispose();
        base.Dispose();
    }

    public void OnDispose()
    {
        Dispose();
    }
}

