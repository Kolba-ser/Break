using Break.Weapons;
using UnityEngine;

namespace Break.Inventory
{
    public abstract class InventoryBase<T> : MonoBehaviour
    {
        public delegate void InventoryUIHandler(IInventoryItemModel<Weapon> itemModel, int slotIndex);

        public event InventoryUIHandler OnAddedEvent;
        public event InventoryUIHandler OnRemovedEvent;

        public abstract int Capacity { get; }

        protected void OnAdded(IInventoryItemModel<Weapon> itemModel, int slotIndex)
        {
            OnAddedEvent?.Invoke(itemModel, slotIndex);
        }
        protected void OnRemoved(IInventoryItemModel<Weapon> itemModel, int slotIndex)
        {
            OnRemovedEvent?.Invoke(itemModel, slotIndex);
        }

        public abstract bool TryPullOut(out T item, T targetItem);
        public abstract bool TryPutIn(T item);
    }
}
