using UnityEngine;

public class Slot<T> : MonoBehaviour
{

    private IEquipable equipable;
    private T item;

    public T Item => item;

    public bool isFree => equipable == null;

    public bool TryEquip(IEquipable equipable, T item)
    {
        if (!isFree)
            return false;

        Debug.Log("TryEquip");
        this.item = item;
        this.equipable = equipable;
        equipable.Put(transform);
        return true;
    }

    public bool TryRemove()
    {
        if (isFree)
            return false;

        equipable = null;
        item = default;
        equipable.PutAway();
        return true;
    }

    public bool TryPullOut(out T item)
    {
        item = default;
        T tempItem = this.item;
        if (TryRemove())
        {
            item = tempItem;
            return true;
        }

        return false;
    }
}

