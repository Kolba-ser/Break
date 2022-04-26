using System;
using UniRx;
using UnityEngine;

namespace Break.Procedural.Animation.SpiderMovement
{
    public sealed class Spider : MonoBehaviour, IKillable
    {
        [SerializeField] private float distanceFromGround;
        [SerializeField] private float upForce;
        [SerializeField] private float stepLength;
        [Space(20)]

        [SerializeField] private Leg[] legs;

        private Rigidbody rigidbody;

        private RaycastHit hit;

        private IDisposable moving;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            Move();
        }

        private void Move()
        {
            moving?.Dispose();

            moving = Observable.EveryUpdate().TakeUntilDisable(gameObject)
                .Subscribe(_ =>
                {
                    for (int i = 0; i < legs.Length; i++)
                    {
                        var leg = legs[i];

                        if (CanTakeStep(leg) && CanMove(i))
                            TakeStep(leg);
                    }
                });
        }

        private void FixedUpdate()
        {
            Physics.Raycast(rigidbody.position, -transform.up, out hit, distanceFromGround, Layers.Instance.Ground);
            var currentDistanceFromGround = (transform.position - hit.point).magnitude;
            if (currentDistanceFromGround < distanceFromGround)
            {
                rigidbody.AddForce(Vectors.Up * upForce, ForceMode.Force);
            }
        }


        private bool CanMove(int index)
        {
            var n1 = legs[(index + legs.Length - 1) % legs.Length];
            var n2 = legs[(index + 1) % legs.Length];
            return !n1.Target.IsMoving && !n2.Target.IsMoving;
        }

        private bool CanTakeStep(in Leg leg)
        {
            var inRange = (leg.Target.CurrentPosition - leg.Raycast.Point).sqrMagnitude > stepLength * stepLength;
            return !leg.Target.IsMoving && inRange;
        }

        private void TakeStep(in Leg leg)
        {
            leg.Target.MoveTo(leg.Raycast.Point);
        }

        public void OnDeath()
        {
            moving?.Dispose();
        }

        [Serializable]
        private struct Leg
        {
            public LegRaycast Raycast;
            public LegTarget Target;
        }
    }
}
