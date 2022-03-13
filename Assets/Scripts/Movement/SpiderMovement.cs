using UnityEngine;

namespace Break.Scripts.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    public sealed class SpiderMovement : MonoBehaviour
    {
        [SerializeField] private float speed;

        [Header("Input settings")]
        [SerializeField] private KeyCode forward;
        [SerializeField] private KeyCode backward;
        [SerializeField] private KeyCode right;
        [SerializeField] private KeyCode left;

        private Rigidbody rigidbody;
        private RaycastHit hit;
        private Vector3 direction;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            direction = new Vector3();
        }

        private void Update()
        {
            var dir1 = System.Convert.ToInt16(Input.GetKey(forward)) + (-System.Convert.ToInt16(Input.GetKey(backward)));
            var dir2 = System.Convert.ToInt16(Input.GetKey(right)) + (-System.Convert.ToInt16(Input.GetKey(left)));

            direction.x = dir2;
            direction.z = dir1;
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
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.FromToRotation(CMath.Up, hit.normal), 5 * Time.deltaTime);


        }


    }
}
