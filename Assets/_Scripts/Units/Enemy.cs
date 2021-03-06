
using Break.Pool;
using UniRx;
using UnityEngine;
using Zenject;

public sealed class Enemy : Damagable, IPooledObject
{
    [SerializeField] private int moneyReward;

    [Inject] private EventHolder eventHolder;
    [Inject] private PoolSystem poolSystem;
    public void OnPullIn()
    {
        
    }

    public void OnPullOut()
    {
        currentHealth = health;
    }

    protected override void OnDeath()
    {
        eventHolder.OnEnemyDie.Invoke(moneyReward);
        
        Observable.Timer(2f.InSec())
            .Subscribe(_ => poolSystem.TryRemove<Enemy>(this));
        base.OnDeath();
    }
}

