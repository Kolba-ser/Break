using Break.Weapons;
using Cinemachine;
using System;
using UniRx;
using UnityEngine;

[RequireComponent(typeof(CinemachineImpulseSource))]
public sealed class Harpoon : Weapon
{
    [SerializeField] private LineRenderer rope;

    [Space(20)]
    [SerializeField] private HarpoonSettings settings;

    private CinemachineImpulseSource impulseSource;
    private Graspable target;
    
    private RaycastHit hit;
    
    private IDisposable grappling;

    private void Awake()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    public override void StartShoot(Action callback = null)
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
            callback?.Invoke();
            impulseSource.GenerateImpulse();
        }
    }
    public override void StopShoot(Action callback = null)
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

