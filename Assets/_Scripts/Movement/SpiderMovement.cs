using UnityEngine;
using UnityEngine.InputSystem;

namespace Break.Scripts.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    public sealed class SpiderMovement : MonoBehaviour
    {
        [SerializeField] private float speed;

        private Rigidbody rigidbody;
        private RaycastHit hit;
        private Vector3 direction;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            direction = new Vector3();
        }

        public void OnMove(InputValue value)
        {
            var v2 = value.Get<Vector2>();
            direction.x = v2.x;
            direction.z = v2.y;
        }

       private void FixedUpdate()
        {
            var ray = new Ray(transform.position, CMath.Down);
            Physics.Raycast(ray, out hit);
            var d = direction - Vector3.Dot(direction, hit.normal) * hit.normal;
            var offset = d * (speed * Time.fixedDeltaTime);
            
            if (direction != CMath.Zero)
            {
                rigidbody.MovePosition(rigidbody.position + offset);
            }


        }



    }
}
