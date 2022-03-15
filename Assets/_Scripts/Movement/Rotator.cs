using UnityEngine;

namespace Assets.Scripts.Movement
{
    public sealed class Rotator : MonoBehaviour
    {
        [SerializeField] private float speed;

        private Camera camera;

        private void Awake()
        {
            camera = Camera.main;
        }


        private void LateUpdate()
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, camera.transform.rotation, speed * Time.deltaTime);
        }
    }
}
