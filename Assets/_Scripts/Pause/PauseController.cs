
using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class PauseController : Singletone<PauseController>, IDisposable
{

    private List<IPauseHandler> handlers = new List<IPauseHandler>();

    private const float NORMAL_TIME = 1f;
    private const float SLOWDOWN_TIME = 0.3f;

    private bool isPaused;

    public bool IsPaused => isPaused;

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

    public void Dispose()
    {
        handlers.Clear();
    }
}

