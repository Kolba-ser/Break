
using UnityEngine;

public sealed class Enemy : Damagable
{
    [SerializeField] private int moneyReward;

    protected override void OnDeath()
    {
        EventHolder.Instance.OnEnemyDie.Invoke(moneyReward);
        base.OnDeath();
    }
}

