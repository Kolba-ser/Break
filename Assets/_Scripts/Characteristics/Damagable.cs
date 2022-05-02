using System;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : Characteristic
{
    [SerializeField] protected float health;

    protected float currentHealth;
    private IKillable[] killables;

    public float CurrentHealth => currentHealth;
    public float InitialHelth => health;
    public bool IsDead => currentHealth <= 0;

    protected virtual void Awake()
    {
        killables = GetComponentsInChildren<IKillable>();
    }

    public void GetDamage(float damage)
    {
        if (IsDead)
            return;

        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            OnDeath();
        }
    }

    protected virtual void OnDeath()
    {
        foreach (var killable in killables)
        {
            killable.OnDeath();
        }
    }
}

