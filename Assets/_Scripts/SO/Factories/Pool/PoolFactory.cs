

using System;
using UnityEngine;

public abstract class PoolFactory : ScriptableObject
{
    public abstract Type ProductType { get; }

    protected bool verified { get; private set; }

    public abstract bool TryCreate(out IPooledObject pooledObject, Transform parent = null);

    protected bool Verify(Transform transform)
    {
        if(!verified && transform.GetComponent<IPooledObject>() != null)
        {
            verified = true;
        }

        return verified;
    }
}
