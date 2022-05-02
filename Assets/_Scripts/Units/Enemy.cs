
using Break.Pool;
using UniRx;
using UnityEngine;

public sealed class Enemy : Damagable, IPooledObject
{
    [SerializeField] private int moneyReward;

    public void OnPullIn()
    {
        
    }

    public void OnPullOut()
    {
        currentHealth = health;
    }

    protected override void OnDeath()
    {
        EventHolder.Instance.OnEnemyDie.Invoke(moneyReward);
        Observable.Timer(2f.InSec())
            .Subscribe(_ => PoolSystem.Instance.TryRemove<Enemy>(this));
        base.OnDeath();
    }
}

