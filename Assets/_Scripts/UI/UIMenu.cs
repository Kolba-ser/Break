using System;
using UnityEngine;


public abstract class UIMenu : MonoBehaviour
{
    protected virtual void Start()
    {
        UIController.Instance.Register(this);
    }

    public abstract Type Type { get; }

    public virtual void Show()
    {
        EventHolder.Instance.OnMenuOpenedEvent.Invoke();
    }

    public virtual void Hide()
    {
        EventHolder.Instance.OnMenuClosedEvent.Invoke();
    }
}

