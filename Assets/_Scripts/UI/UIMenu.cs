using System;
using UnityEngine;
using Zenject;

public abstract class UIMenu : MonoBehaviour
{

    [Inject] protected InputService inputService;
    [Inject] private EventHolder eventHolder;

    protected virtual void Start()
    {
        UIController.Instance.Register(this);
    }

    public abstract Type Type { get; }

    public virtual void Show()
    {
        eventHolder.OnMenuOpenedEvent.Invoke();
    }

    public virtual void Hide()
    {
        eventHolder.OnMenuClosedEvent.Invoke();
    }
}

