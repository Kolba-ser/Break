using UnityEngine;

namespace Break.Procedural.Animation.SpiderMovement
{

    public sealed class LegTarget : MonoBehaviour
    {
        [SerializeField] private float stepSpeed;
        [SerializeField] private AnimationCurve stepCurve;

        private Movement? movement;

        public Vector3 CurrentPosition
        {
            get; private set;
        }
        public bool IsMoving => movement != null;

        private void Awake()
        {
            CurrentPosition = transform.position;
        }

        private void Update()
        {
            if (this.movement != null)
            {
                var movement = this.movement.Value;

                movement.Progress = Mathf.Clamp01(movement.Progress + Time.deltaTime * stepSpeed);
                CurrentPosition = movement.Evaluate(stepCurve);
                this.movement = movement.Progress < 1 ? movement : null;
            }

            transform.position = CurrentPosition;
        }

        public void MoveTo(Vector3 destination)
        {
            if (movement == null)
            {
                movement = new Movement
                {
                    Progress = 0,
                    FromPosition = CurrentPosition,
                    ToPosition = destination
                };
            }
            else
            {
                movement = new Movement
                {
                    Progress = movement.Value.Progress,
                    FromPosition = movement.Value.FromPosition,
                    ToPosition = destination
                };
            }
        }

        private struct Movement
        {
            public float Progress;
            public Vector3 FromPosition;
            public Vector3 ToPosition;

            public Vector3 Evaluate(AnimationCurve curve)
            {
                return Vector3.Lerp(FromPosition, ToPosition, Progress) + CMath.Up * curve.Evaluate(Progress);
            }
        }
    }
}