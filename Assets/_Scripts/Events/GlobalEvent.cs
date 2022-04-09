
using System;
using System.Collections.Generic;

public sealed class GlobalEvent : IDisposable
{
    private List<(Action, int)> subscribers = new List<(Action, int)>();

    private int amount;

    public void Subscribe(Action callback, int priority = 0)
    {
        int index = 0;
        
        for (int i = 0; i < amount; i++)
        {
            int currentPriority = subscribers[i].Item2;
            if (currentPriority < priority)
            {
                index = i;
                break;
            }
        }

        subscribers.Insert(index, (callback, priority));
        amount++;
    }
    public void Unsubscribe(Action callback)
    {
        if (subscribers.RemoveAll(pair => ReferenceEquals(pair.Item1, callback)).AsBool())
            amount--;
    }

    public void Invoke()
    {
        for (int i = 0; i < amount; i++)
        {
            subscribers[i].Item1?.Invoke();
        }
    }

    public void Dispose()
    {
        subscribers.Clear();
    }
}

