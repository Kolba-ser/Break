using UnityEngine;

namespace Break.Inventory
{
    public abstract class InventoryBase<T> : MonoBehaviour
    {
        public abstract bool TryPullOut(out T item, T targetItem);
        public abstract bool TryPutIn(T item);
        public abstract bool TryRemove(T item);
    }
}
