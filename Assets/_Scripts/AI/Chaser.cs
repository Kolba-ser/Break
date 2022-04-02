
using System;
using UniRx;

public sealed class Chaser : AIDependet
{
    private IDisposable chasing;

    public void Chase()
    {
        chasing?.Dispose();

        chasing = Observable.Interval(0.2f.InSec()).TakeUntilDisable(target.transform)
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
}

