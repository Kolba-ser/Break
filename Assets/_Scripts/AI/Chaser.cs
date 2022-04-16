
using System;
using UniRx;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public sealed class Chaser : AIDependet, IKillable
{
    private IDisposable chasing;
    private IDisposable rotation;

    private void Chase()
    {
        chasing?.Dispose();
        rotation?.Dispose();

        chasing = Observable.Interval(0.2f.InSec()).TakeUntilDisable(target.transform).TakeUntilDisable(gameObject)
            .TakeWhile(_ => target != null)
            .Subscribe(_ =>
            {
                agent.SetDestination(target.transform.position);
            });
    }

    protected override void OnEnter(ITargetable target)
    {
        base.OnEnter(target);
        Chase();
    }

    public override void OnDeath()
    {
        base.OnDeath();
        chasing?.Dispose();
    }
}

