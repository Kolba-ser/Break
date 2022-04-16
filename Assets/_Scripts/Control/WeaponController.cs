﻿using Break.Weapons;
using System;
using UnityEngine;


public abstract class WeaponController : MonoBehaviour
{
    [SerializeField] protected Weapon activeWeapon;
    [SerializeField] protected Aim aim;

    protected Action<float> OnShot;

    public Weapon ActiveWeapon => activeWeapon;
    public bool IsWeaponUsed => activeWeapon != null;

    public void Subscribe(Action<float> action)
    {
        OnShot += action;
    }

    public void Unsubscribe(Action<float> action)
    {
        OnShot -= action;
    }

    public void SetWeapon(Weapon weapon)
    {
        if (weapon == null)
        {
            Debug.LogError("The weapon is null");
            return;
        }
        if (activeWeapon != null)
        {
            activeWeapon.RemoveAim();
            activeWeapon.Deactivate();
        }

        activeWeapon = weapon;
        activeWeapon.SetAim(aim);
        activeWeapon.Activate();
        aim.Weapon = weapon;
        aim.LookAtMouse();
    }
    public void ResetWeapon()
    {
        if (activeWeapon != null)
        {
            activeWeapon.RemoveAim();
            aim.Weapon = null;
            activeWeapon = null;
        }
    }

}

