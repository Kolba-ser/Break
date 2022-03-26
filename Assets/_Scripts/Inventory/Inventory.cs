using Break.Inventory;
using Break.Weapons;
using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class Inventory : InventoryBase<Weapon>
{
    [SerializeField] private List<WeaponSlot> slots;

    public int Capacity => slots.Count;

    private bool isFull;

    public override bool TryPutIn(Weapon item)
    {
        Debug.Log(item.TryGetComponent(out IEquipable _));
        Debug.Log(TryGetFirstFree(out WeaponSlot _));
        Debug.Log(!isFull);

        if (!isFull && item.TryGetComponent(out IEquipable equipable) && TryGetFirstFree(out WeaponSlot freesSlot))
        {
            Debug.Log("Инветарь");
            return freesSlot.TryEquip(equipable, item);
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
            return true;
        }

        return false;
    }
    public override bool TryRemove(Weapon targetItem)
    {
        var targetSlot = slots.Find(_ => _.Item == targetItem);
        if (targetSlot != null && targetSlot.TryRemove())
        {
            isFull = false;
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

