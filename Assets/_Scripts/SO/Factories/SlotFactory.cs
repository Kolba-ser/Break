using Break.Factories;
using System;
using UnityEngine;

[CreateAssetMenu(fileName ="Factory/SlotFactory")]
public sealed class SlotFactory : FactoryBase
{
    [SerializeField] private InventorySlotPresenter slotPresenter;

    public override Type ProductType => typeof(InventorySlotPresenter);

    public override Transform Create(Transform parent = null)
    {
        return Instantiate(slotPresenter.transform, parent);
    }
}
