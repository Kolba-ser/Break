using Break.Weapons;
using System;
using UnityEngine;

public sealed class PlayerWeaponController : WeaponController, IPauseHandler
{

    private bool isPaused => PauseController.Instance.IsPaused;

    private void Start()
    {
        InputController.Instance.OnFire(OnFireStart, OnFireStop);
        PauseController.Instance.Register(this);
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

    public void OnPaused()
    {
        aim.Dispose();
    }

    public void OnUnpaused()
    {
        aim.LookAtMouse();
    }
}

