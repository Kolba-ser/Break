using Break.Pool;
using System;
using UnityEngine;


public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected float lifeTime;

    protected void ReturnToPool<T>(Transform pooledObject, Action callback = null)
    {
        if (PoolSystem.Instance.TryRemove<T>(pooledObject))
        {
            callback?.Invoke();
        }

    }
}

