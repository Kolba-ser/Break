
using System.Collections.Generic;

public sealed class Disposer : MonoSingleton<Disposer>
{

    private List<IDisposeHandler> handlers = new List<IDisposeHandler>();

    private void Start()
    {
        Register(EventHolder.Instance);
        Register(PauseController.Instance);
    }

    public void Register(IDisposeHandler handler)
    {
        handlers.Add(handler);
    }
    
    private void Notify()
    {
        foreach (var item in handlers)
        {
            item.OnDispose();
        }
    }

    private void OnDestroy()
    {
        Notify();
    }
}

