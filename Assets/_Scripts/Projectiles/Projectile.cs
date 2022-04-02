using Break.Pool;
using System;
using UniRx;
using UnityEngine;


public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected float lifeTime;

    protected float damage;

    protected void ReturnToPool<T>(Transform pooledObject, Action callback = null)
    {
        if (PoolSystem.Instance.TryRemove<T>(pooledObject))
        {
            callback?.Invoke();
        }
    }


    protected void TakeDamage(Transform target)
    {
        if (target.TryGetComponent(out Damagable damagable))
        {
            damagable.GetDamage(damage);
        }
    }

    protected virtual void OnReturned()
    {
        damage = 0;
    }
}

