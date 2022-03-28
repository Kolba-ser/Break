using Break.Pool;
using Break.Projectiles;
using Break.Weapons;
using UnityEngine;

public sealed class Railgun : Weapon
{
    [SerializeField] private float force;

    public override void StartShoot()
    {
        base.StartShoot();
        if (PoolSystem.Instance.TryGet(out Armature armature))
        {
            armature.transform.position = shotPoint.position;
            armature.transform.rotation = shotPoint.rotation;
            armature.Rigidbody.AddForce(force * transform.forward, ForceMode.Impulse);
        }
    }



}

