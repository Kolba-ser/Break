
using UnityEngine;

public interface IPooledObject<T>
{
    public T Component { get; }

    public Transform transform { get; }
    public void OnGet();
    public void OnRemove();
}

