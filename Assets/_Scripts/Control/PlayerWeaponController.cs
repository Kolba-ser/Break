using Break.Weapons;
using System;
using UnityEngine;

public sealed class PlayerWeaponController : WeaponController
{

    private bool isPaused => PauseController.Instance.IsPaused;

    private void Start()
    {
        InputController.Instance.OnFire(OnFireStart, OnFireStop);
    }

    private void OnFireStart()
    {
        if (activeWeapon == null || activeWeapon.IsShooting || !activeWeapon.IsActive || isPaused)
            return;

        activeWeapon.StartShoot(OnShot);
    }

    private void OnFireStop()
    {
        if (activeWeapon == null || !activeWeapon.IsShooting || isPaused)
            return;

        activeWeapon.StopShoot(OnShot);
    }

    public override void OnPaused()
    {
        base.OnPaused();
        activeWeapon.StopShoot();
    }
}

