
using UnityEngine;

namespace Break.Triggers
{
    [RequireComponent(typeof(SphereCollider))]
    public abstract class PickupTrigger<T> : MonoBehaviour
    {
        public SphereCollider collider { get; private set; }

        public delegate void PickupHandler();
        public abstract event PickupHandler OnPickedUpEvent;

        public delegate void PickupHandlerDebug(T item);
        public abstract event PickupHandlerDebug OnPickedUpEventDebug;

        protected void Awake()
        {
            collider = GetComponent<SphereCollider>();
        }
    }
}
