using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class Damagable : Characteristic
{
    [SerializeField] private float health;

    private IKillable[] killables;

    private void Awake()
    {
        killables = GetComponentsInChildren<IKillable>();
    }

    public void GetDamage(float damage)
    {
        health -= damage;

        if(health <= 0)
        {
            OnDeath();
        }
    }

    private void OnDeath()
    {
        foreach (var killable in killables)
        {
            killable.OnDeath();
        }
    }
}

