
using Break.Inventory;
using Break.Weapons;
using System;
using UnityEngine;

public sealed class EquipmentController : MonoBehaviour
{
    [SerializeField] private WeaponController weaponController;
    [SerializeField] private InventoryBase<Weapon> inventory;

    private void OnEnable()
    {
        inventory.OnRemovedEvent += OnRemove;
    }
    private void OnDisable()
    {
        inventory.OnRemovedEvent -= OnRemove;
    }

    private void OnRemove(IInventoryItemModel<Weapon> itemModel, int slotIndex)
    {
        if (weaponController.ActiveWeapon.Equals(itemModel.Component))
        {
            weaponController.RemoveWeapon();
        }
    }
}

