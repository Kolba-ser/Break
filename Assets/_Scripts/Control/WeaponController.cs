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
            activeWeapon.RemoveAim();

        activeWeapon = weapon;
        activeWeapon.SetAim(aim);
        aim.Weapon = weapon;
    }

    private void OnFireStart()
    {
        if (activeWeapon == null || activeWeapon.isShooting)
            return;

        activeWeapon.StartShoot(OnShot);
    }

    private void OnFireStop()
    {
        if (activeWeapon == null || !activeWeapon.isShooting)
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

