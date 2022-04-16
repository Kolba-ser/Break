using Break.Inventory;
using Break.Weapons;
using System.Collections.Generic;
using UnityEngine;

public sealed class Inventory : InventoryBase<Weapon>, IInventoryModel
{
    [SerializeField] private List<WeaponSlot> slots;

    public override int Capacity => slots.Count;

    private bool isFull;

    private void Awake()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].Index = i;
        }
    }

    public override bool TryPutIn(Weapon item)
    {
        if (!isFull && item.TryGetComponent(out IEquipable equipable) && TryGetFirstFree(out WeaponSlot freeSlot))
        {
            if(!equipable.IsPlaced && freeSlot.TryEquip(equipable, item))
            {
                item.gameObject.SetActive(false);
                OnAdded(item, freeSlot.Index);
                return true;
            }
        }

        return false;
    }
    public override bool TryPullOut(out Weapon weapon, Weapon targetItem)
    {
        weapon = null;
        var targetSlot = slots.Find(_ => _.Item == targetItem);

        if (targetSlot != null && targetSlot.TryPullOut(out weapon))
        {
            isFull = false;
            OnRemoved(weapon, targetSlot.Index);
            return true;
        }

        return false;
    }

    private bool TryGetFirstFree(out WeaponSlot freeSlot)
    {
        freeSlot = null;

        for (int i = 0; i < Capacity; i++)
        {
            var slot = slots[i];
            if (slot.isFree)
            {
                freeSlot = slot;
                return true;
            }
        }

        isFull = true;
        return false;
    }

}

