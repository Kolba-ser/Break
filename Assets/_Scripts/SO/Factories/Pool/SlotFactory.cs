using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Factory/SlotFactory")]
public sealed class SlotFactory : PoolFactory
{
    [SerializeField] private InventorySlotPresenter slotPresenter;

    public override Type ProductType => typeof(InventorySlotPresenter);

    public override bool TryCreate(out IPooledObject pooledObject, Transform parent = null)
    {
        pooledObject = null;

        if (verified || Verify(slotPresenter.transform))
        {
            pooledObject = Instantiate(slotPresenter, parent);
            return true;
        }

        return false;

    }
}
