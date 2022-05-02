using System;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : Characteristic
{
    [SerializeField] private float health;

    private float currentHealth;
    private IKillable[] killables;

    public float CurrentHealth => currentHealth;
    public float InitialHelth => health;
    public bool IsDead => currentHealth <= 0;


    private void Awake()
    {
        killables = GetComponentsInChildren<IKillable>();
    }

    private void OnEnable()
    {
        currentHealth = health;
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

