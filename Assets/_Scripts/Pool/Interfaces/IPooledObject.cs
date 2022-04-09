
using UnityEngine;

public interface IPooledObject
{
    public Transform transform { get; }
    public void OnPullIn();
    public void OnPullOut();
}

