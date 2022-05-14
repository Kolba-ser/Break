
using UnityEngine;
using Zenject;

public sealed class Player : Damagable
{
    [Inject] private EventHolder eventHolder;

    protected override void Awake()
    {
        currentHealth = health;
        base.Awake();
    }

    protected override void OnDeath()
    {
        eventHolder.OnEndGame.Invoke(false);
        base.OnDeath();
    }
}

