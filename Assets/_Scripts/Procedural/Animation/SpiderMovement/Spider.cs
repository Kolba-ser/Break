using System;
using UnityEngine;

namespace Break.Procedural.Animation.SpiderMovement
{
    public sealed class Spider : MonoBehaviour
    {
        [SerializeField] private float stepLength;

        [Space(20)]
        [SerializeField] private Leg[] legs;

        private void Update()
        {
            for (int i = 0; i < legs.Length; i++)
            {
                var leg = legs[i];

                if (CanTakeStep(leg) && CanMove(i))
                    TakeStep(leg);
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

        [Serializable]
        private struct Leg
        {
            public LegRaycast Raycast;
            public LegTarget Target;
        }
    }
}
