using Break.Inventory;
using Break.Weapons;
using UnityEngine;

namespace Break.Triggers
{
    public sealed class WeaponPickupTrigger : PickupTrigger<Weapon>
    {
        [SerializeField] private InventoryBase<Weapon> inventory;

        public override event PickupHandler OnPickedUpEvent;
        public override event PickupHandlerDebug OnPickedUpEventDebug;

        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out Weapon weapon) && inventory.TryPutIn(weapon))
            {
                Debug.Log("оружее поднято");
                OnPickedUpEventDebug?.Invoke(weapon);
            }
        }
    }
}
