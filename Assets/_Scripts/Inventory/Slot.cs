using UnityEngine;

public class Slot<T> : MonoBehaviour
{

    private IEquipable equipable;
    private T item;

    private int index;

    public T Item => item;

    public bool isFree => equipable == null;

    public int Index
    {
        get
        {
            return index;
        }
        set
        {
            if(value >= 0)
            {
                index = value;
            }
        }
    }

    public bool TryEquip(IEquipable equipable, T item)
    {
        if (!isFree)
            return false;
        Debug.Log(name);
        this.item = item;
        this.equipable = equipable;
        equipable.Put(transform);
        return true;
    }

    public bool TryRemove()
    {
        if (isFree)
            return false;

        equipable.PutAway();
        equipable = null;
        item = default;
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

