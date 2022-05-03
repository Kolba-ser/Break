using Break.Pool;
using Break.Projectiles;
using Break.Weapons;
using UnityEngine;
using Cinemachine;
using System;
using UniRx;

[RequireComponent(typeof(CinemachineImpulseSource))]
public sealed class Railgun : Weapon
{
    [SerializeField] private float minForce;
    [SerializeField] private float maxForce;
    [SerializeField] private float accambulationRate;

    private CinemachineImpulseSource impulseSource;

    private float currentLaunchForce;
    private float currentRecoilForce;

    private IDisposable accambulating;

    private const float MULTIPLIER = 0.01f;

    protected override void Awake()
    {
        base.Awake();
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    public override void StartShoot(Action<float> recoilCallback = null)
    {
        base.StartShoot();
        currentLaunchForce = minForce;
        if (PoolSystem.Instance.CanGet<Armature>())
        {
            accambulating = Observable.EveryUpdate().TakeUntilDisable(gameObject)
                .Subscribe(_ =>
                {
                    currentLaunchForce = Mathf.Lerp(currentLaunchForce, maxForce, accambulationRate * Time.deltaTime);
                });
        }
    }

    public override void StopShoot(Action<float> recoilCallback = null)
    {
        base.StopShoot();
        accambulating?.Dispose();
        if (PoolSystem.Instance.TryGet(out Armature armature))
        {
            armature.transform.position = shotPoint.position;
            armature.transform.rotation = shotPoint.rotation;
            armature.Launch(damage * MULTIPLIER, currentLaunchForce * transform.forward);
            impulseSource.GenerateImpulse();
            currentRecoilForce = Mathf.Clamp(recoilForce * currentLaunchForce.InPercent(minForce, maxForce) , 0, RecoilForce);
            recoilCallback?.Invoke(currentRecoilForce);
        }
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

