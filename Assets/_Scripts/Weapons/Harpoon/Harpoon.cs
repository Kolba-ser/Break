using Break.Weapons;
using System;
using UniRx;
using UnityEngine;


public sealed class Harpoon : Weapon
{
    [SerializeField] private Transform shotPoint;

    [SerializeField] private LineRenderer rope;

    [Space(20)]
    [SerializeField] private HarpoonSettings settings;

    private RaycastHit hit;
    private Graspable target;
    private IDisposable grappling;



    public override void StartShoot()
    {
        Physics.Raycast(shotPoint.position, shotPoint.forward, out hit, settings.RopeLength, settings.Graspable);

        if (isAimSet && hit.collider && hit.collider.TryGetComponent(out Graspable graspable))
        {
            base.StartShoot();
            target = graspable;

            var direction = new Direction(target.transform, shotPoint);

            target.Join(direction, settings.GrappingForce);
            ShowRope();
            aim.LookAtTarget(target.transform);
        }
    }
    public override void StopShoot()
    {
        base.StopShoot();

        target.Execute();
        aim.LookAtMouse();
        HideRope();
    }

    private void ShowRope()
    {
        rope.enabled = true;
        grappling = Observable.EveryLateUpdate()
            .TakeUntilDisable(gameObject)
            .TakeWhile(_ => (target.transform.position - shotPoint.position).magnitude < settings.RopeLength)
            .Finally(() => StopShoot())
            .Subscribe(_ =>
            {
                rope.SetPosition(0, shotPoint.position);
                rope.SetPosition(1, target.transform.position);
            });
    }

    private void HideRope()
    {
        grappling?.Dispose();
        rope.enabled = false;
    }


}

