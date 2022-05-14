
using Break.Pause;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public sealed class PauseService :IDisposable, IPauseService
{

    private List<IPauseHandler> handlers = new List<IPauseHandler>();

    private EventHolder eventHolder;

    private const float NORMAL_TIME = 1f;
    private const float SLOWDOWN_TIME = 0.3f;

    private bool isPaused;

    public bool IsPaused => isPaused;

    [Inject]
    private void Construct(EventHolder eventHolder)
    {
        this.eventHolder = eventHolder;
        this.eventHolder.OnLevelChanged.Subscribe(Unpause);
    }

    public void Register(IPauseHandler handler)
    {
        handlers.Add(handler);
    }
    public void Unregister(IPauseHandler handler)
    {
        handlers.Remove(handler);
    }

    public void Pause()
    {
        if (!isPaused)
        {
            Time.timeScale = SLOWDOWN_TIME;
            isPaused = true;
            Notify();
        }
    }
    public void Unpause()
    {
        if (isPaused)
        {
            Time.timeScale = NORMAL_TIME;
            isPaused = false;
            Notify();
        }
    }

    private void Notify()
    {
        foreach (var handler in handlers)
        {
            if (isPaused)
            {
                handler.OnPaused();
            }
            else
            {
                handler.OnUnpaused();
            }
        }
    }

    void IDisposable.Dispose()
    {
        handlers.Clear();
        Unpause();
    }
}

