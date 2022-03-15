using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Break.Scripts.Control
{
    public sealed class AimController : MonoBehaviour
    {
        public Vector2 _look;

        public Vector3 nextPosition;
        public Quaternion nextRotation;

        public float rotationPower = 3f;
        public float rotationLerp = 0.5f;

        public void OnLook(InputValue value)
        {
            _look = value.Get<Vector2>();
        }


        public GameObject followTransform;

        private void Update()
        {
            followTransform.transform.rotation *= Quaternion.AngleAxis(_look.x * rotationPower, Vector3.up);

            followTransform.transform.rotation *= Quaternion.AngleAxis(_look.y * rotationPower, Vector3.right);

            var angles = followTransform.transform.localEulerAngles;
            angles.z = 0;

            var angle = followTransform.transform.localEulerAngles.x;

            if (angle > 180 && angle < 340)
            {
                angles.x = 340;
            }
            else if (angle < 180 && angle > 40)
            {
                angles.x = 40;
            }


            followTransform.transform.localEulerAngles = angles;

            transform.rotation = Quaternion.Euler(0, followTransform.transform.rotation.eulerAngles.y, 0);
            followTransform.transform.localEulerAngles = new Vector3(angles.x, 0, 0);
        }
    }
}