using Break.Weapons;
using System;
using UnityEngine;

public sealed class WeaponController : MonoBehaviour
{
    [SerializeField] private Weapon activeWeapon;
    [SerializeField] private Aim aim;

    private Action OnShot;

    public Weapon ActiveWeapon => activeWeapon;

    private void Start()
    {
        InputController.Instance.OnFire(OnFireStart, OnFireStop);
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
    }
    public void ResetWeapon()
    {
        if (activeWeapon != null)
        {
            activeWeapon.RemoveAim();
            activeWeapon = null;
        }
    }

    private void OnFireStart()
    {
        if (activeWeapon == null || activeWeapon.IsShooting || !activeWeapon.IsActive)
            return;

        activeWeapon.StartShoot(OnShot);
    }

    private void OnFireStop()
    {
        if (activeWeapon == null || !activeWeapon.IsShooting)
            return;

        activeWeapon.StopShoot(OnShot);
    }

    public void Subscribe(Action action)
    {
        OnShot += action;
    }

    public void Unsubscribe(Action action)
    {
        OnShot -= action;
    }

}

