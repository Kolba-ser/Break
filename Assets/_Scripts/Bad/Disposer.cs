
using Break.Pause;
using System.Collections.Generic;
using Zenject;

public sealed class Disposer : MonoSingleton<Disposer>
{

    private List<IDisposeHandler> handlers = new List<IDisposeHandler>();


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

