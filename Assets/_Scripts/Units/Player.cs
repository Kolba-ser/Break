
using UnityEngine;

public sealed class Player : Damagable
{

    protected override void Awake()
    {
        currentHealth = health;
        base.Awake();
    }

    protected override void OnDeath()
    {
        EventHolder.Instance.OnEndGame.Invoke(false);
        base.OnDeath();
    }
}

