using UnityEngine;

namespace Break.Scripts.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    public sealed class SpiderMovement : MonoBehaviour
    {
        [SerializeField] private float speed;

        private Controls controls;
        private Rigidbody rigidbody;
        private RaycastHit hit;

        private void Awake()
        {
            controls = new Controls();
            controls.Enable();
            rigidbody = GetComponent<Rigidbody>();
        }

        private void OnDestroy()
        {
            controls.Dispose();
        }

        private void FixedUpdate()
        {
            var direction = new Vector3(controls.Player.LeftRight.ReadValue<float>(),
                0,
                controls.Player.ForwardBackward.ReadValue<float>());

            var ray = new Ray(transform.position, CMath.Down);
            Physics.Raycast(ray, out hit);
            var d = direction - Vector3.Dot(direction, hit.normal) * hit.normal;
            var offset = d * (speed * Time.fixedDeltaTime);
            if (direction != CMath.Zero)
            {
                rigidbody.MovePosition(rigidbody.position + offset);
            }
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.FromToRotation(CMath.Up, hit.normal), 5 * Time.deltaTime);


        }


    }
}
