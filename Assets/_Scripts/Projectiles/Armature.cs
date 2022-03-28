
using System;
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

        public void OnEnable()
        {
            Observable.Timer(lifeTime.InSec())
                .Subscribe(_ => 
                {
                    ReturnToPool<Armature>(transform, OnReturned);
                });
        }

        private void OnCollisionEnter(Collision collision)
        {
            Observable.TimerFrame(2).Subscribe(_ =>
            {
                rigidbody.isKinematic = true;
                transform.SetParent(collision.transform);
                collider.enabled = false;
            });
        }

        private void OnReturned()
        {
            rigidbody.isKinematic = false;
            collider.enabled = true;
        }
    }
}
