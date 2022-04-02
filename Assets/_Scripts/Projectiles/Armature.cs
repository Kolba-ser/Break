using UniRx;
using UnityEngine;

namespace Break.Projectiles
{
    [RequireComponent(typeof(Rigidbody))]
    public sealed class Armature : Projectile
    {
        [SerializeField] private Rigidbody rigidbody;
        [SerializeField] private Collider collider;

        public Rigidbody Rigidbody => rigidbody;

        private void OnCollisionEnter(Collision collision)
        {
            Observable.TimerFrame(2).Subscribe(_ =>
            {
                rigidbody.isKinematic = true;
                transform.SetParent(collision.transform);
                collider.enabled = false;
                TakeDamage(collision.transform);
            });
        }

        public void OnEnable()
        {
            Observable.Timer(lifeTime.InSec())
                .Subscribe(_ =>
                {
                    ReturnToPool<Armature>(transform, OnReturned);
                });
        }

        public void Launch(float damage, Vector3 force)
        {
            if (damage >= 0)
            {
                this.damage = damage * force.Lenght() * rigidbody.mass;
                rigidbody.AddForce(force, ForceMode.Impulse);
            }
        }

        protected override void OnReturned()
        {
            base.OnReturned();
            rigidbody.isKinematic = false;
            collider.enabled = true;
        }


    }
}
