using UnityEngine;

namespace Break.Procedural.Animation.SpiderMovement
{
    public sealed class LegRaycast : MonoBehaviour
    {
        private RaycastHit hit;

        public Vector3 Point => hit.point;
        public Vector3 Normal => hit.normal;

        private void Update()
        {
            var ray = new Ray(transform.position, CMath.Down);
            Physics.Raycast(ray, out hit);
        }
    }
}
