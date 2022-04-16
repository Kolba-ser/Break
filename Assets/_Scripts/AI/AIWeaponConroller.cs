
using Break.Weapons;
using System;
using UniRx;
using UnityEngine;

public sealed class AIWeaponConroller : AIDependet, IDisposable
{
    [SerializeField] private Weapon weapon;
    [SerializeField] private Aim aim;

    [Space(20)]
    [SerializeField] private float startFireInterval;
    [SerializeField] private float stopFireTimer;

    private IDisposable onFireStart;
    private IDisposable onFireStop;

    protected override void Awake()
    {
        base.Awake();
        aim.Weapon = weapon;
        weapon.Put(transform);
    }

    protected override void OnEnter(ITargetable target)
    {
        base.OnEnter(target);
        Dispose();
        Shoot();
        aim.LookAtTarget(target.transform);
    }
    protected override void OnExit()
    {
        base.OnExit();
        Dispose();
        aim.Dispose();
    }

    /// <summary>
    /// Эмулирует нажатие кнопки
    /// </summary>
    private void Shoot()
    {
        onFireStart?.Dispose();
        onFireStart = Observable.Interval(startFireInterval.InSec()).TakeUntilDisable(weapon)
            .SkipWhile(_ => weapon.IsShooting || !weapon.IsActive)
            .Subscribe(_ =>
            {
                weapon.StartShoot();
                onFireStop = Observable.Timer(stopFireTimer.InSec())
                    .Finally(() => weapon.StopShoot())
                    .Subscribe();
            });


    }

    public override void OnDeath()
    {
        base.OnDeath();
        Dispose();
        aim.Weapon = null;
    }

    public void Dispose()
    {
        onFireStart?.Dispose();
        onFireStop?.Dispose();
    }
}

