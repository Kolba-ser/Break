using Break.Pause;
using Break.Weapons;
using System;
using UnityEngine;
using Zenject;

public sealed class PlayerWeaponController : WeaponController
{

    [Inject] private IPauseService pauseService;

    private bool isPaused => pauseService.IsPaused;

    private void Start()
    {
        inputService.OnFire(OnFireStart, OnFireStop);
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

