using Break.Inventory;
using Break.Triggers;
using Break.Weapons;
using System;
using UnityEngine;

namespace Break.Control
{
    public sealed class WeaponPickupController : MonoBehaviour
    {
        [SerializeField] private WeaponController controller;
        [SerializeField] private PickupTrigger<Weapon> trigger;
        [SerializeField] private InventoryBase<Weapon> inventory;

        private void OnEnable()
        {
            trigger.OnPickedUpEventDebug += OnPickedUp;
        }
        private void OnDisable()
        {
            trigger.OnPickedUpEventDebug -= OnPickedUp;
        }

        private void OnPickedUp(Weapon weapon)
        {
            if (!controller.IsWeaponUsed)
            {
                controller.SetWeapon(weapon);
            }
        }
    }
}
