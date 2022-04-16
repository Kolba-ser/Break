using Break.Weapons;
using Cinemachine;
using System;
using UniRx;
using UnityEngine;

[RequireComponent(typeof(CinemachineImpulseSource))]
public sealed class Harpoon : Weapon
{
    [SerializeField] private LineRenderer rope;
    [SerializeField] private float throwForce;

    [Space(20)]
    [SerializeField] private HarpoonSettings settings;

    private CinemachineImpulseSource impulseSource;
    private Graspable target;
    private Rigidbody rigidbody;

    private RaycastHit hit;
    
    private IDisposable grappling;

    private void Awake()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
        rigidbody = GetComponent<Rigidbody>();
    }

    public override void StartShoot(Action<float> callback = null)
    {
        Physics.Raycast(shotPoint.position, shotPoint.forward, out hit, settings.RopeLength, settings.Graspable);

        if (IsAimSet && hit.collider && hit.collider.TryGetComponent(out Graspable graspable))
        {
            base.StartShoot();
            target = graspable;

            var direction = new Direction(target.transform, shotPoint);

            target.Join(direction, settings.GrappingForce);
            ShowRope();
            aim.LookAtTarget(target.transform);
            callback?.Invoke(recoilForce);
            impulseSource.GenerateImpulse();
        }
    }
    public override void StopShoot(Action<float> callback = null)
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

    public override void Put(Transform parent)
    {
        rigidbody.isKinematic = true;
        rigidbody.useGravity = false;
        base.Put(parent);
    }
    public override void PutAway()
    {
        rigidbody.isKinematic = false;
        rigidbody.useGravity = true;
        rigidbody.AddForce(transform.forward * throwForce, ForceMode.Impulse);
        base.PutAway();
    }

}

