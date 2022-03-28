
using UnityEngine;

namespace Break.Projectiles
{
    [RequireComponent(typeof(Rigidbody))]
    public sealed class Armature : Projectile
    {
        private Rigidbody rigidbody;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            rigidbody.isKinematic = true;
        }
    }
}
