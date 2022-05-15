using Break.Pool;
using Break.Units;
using Break.Weapons;
using Cinemachine;
using System;
using UniRx;
using UnityEngine;
using Zenject;

public sealed class G12 : Weapon
{
    [SerializeField] private ParticleSystem bulletHole;
    [SerializeField] private Animator animator;
    [SerializeField] private float fireRate;
    [SerializeField, Range(1, 4)] private int level = 1;

    [Inject] private PoolSystem poolSystem;

    private IDisposable shooting;
    private CinemachineImpulseSource impulseSource;

    private string impactVfx = "G12_Shoot_Vfx";

    protected override void Awake()
    {
        base.Awake();
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    public override void StartShoot(Action<float> recoilCallback = null)
    {
        base.StartShoot();

        animator.SetBool("IsShooting", IsShooting);
        animator.SetInteger("Level", level);

        shooting = Observable.Interval(fireRate.InSec())
            .TakeUntilDisable(gameObject)
            .Subscribe(_ =>
            {

                if (Physics.Raycast(shotPoint.position, transform.forward, out RaycastHit hit, 100f))
                {
                    if(poolSystem.TryGet(out Vfx vfx, impactVfx))
                    {
                        vfx.transform.rotation = Quaternion.LookRotation(hit.normal);
                        vfx.transform.position = hit.point;
                    }

                    if (hit.collider.TryGetComponent(out Damagable damagable))
                    {
                        damagable.GetDamage(damage);
                    }
                }

                recoilCallback(RecoilForce);
                impulseSource.GenerateImpulse();

            });
    }
    public override void StopShoot(Action<float> recoilCallback = null)
    {
        base.StopShoot();
        animator.SetBool("IsShooting", IsShooting);
        shooting?.Dispose();
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

