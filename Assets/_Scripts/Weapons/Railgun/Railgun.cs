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
    [SerializeField] private float minforce;
    [SerializeField] private float maxforce;
    [SerializeField] private float accambulationRate;

    private CinemachineImpulseSource impulseSource;

    private float currentForce;

    private IDisposable accambulating;

    private const float MULTIPLIER = 0.01f;

    private void Awake()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    public override void StartShoot(Action callback = null)
    {
        base.StartShoot();
        currentForce = minforce;
        if (PoolSystem.Instance.CanGet<Armature>())
        {
            accambulating = Observable.EveryUpdate().TakeUntilDisable(gameObject)
                .Subscribe(_ =>
                {
                    currentForce = Mathf.Lerp(currentForce, maxforce, accambulationRate * Time.deltaTime);
                });
        }
    }

    public override void StopShoot(Action callback = null)
    {
        base.StopShoot();
        accambulating?.Dispose();

        if (PoolSystem.Instance.TryGet(out Armature armature))
        {
            armature.transform.position = shotPoint.position;
            armature.transform.rotation = shotPoint.rotation;
            armature.Launch(damage * MULTIPLIER, currentForce * transform.forward);
            impulseSource.GenerateImpulse();
            callback?.Invoke();
        }
    }





}

