using Break.Inventory;
using Break.Pool;
using Break.Weapons;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public sealed class InventoryPresenter : UIMenu
{
    [SerializeField] private InventoryBase<Weapon> inventoryModel;
    [Space(20)]

    [SerializeField] private Transform content;
    [SerializeField] private PlayerWeaponController weaponController;

    [Inject] private EventHolder eventHolder;

    private List<InventorySlotPresenter> slots;
    private Canvas canvas;

    public override Type Type => typeof(InventoryPresenter);

    private void Awake()
    {
        slots = new List<InventorySlotPresenter>(inventoryModel.Capacity);
        canvas = GetComponent<Canvas>();
    }

    protected override void Start()
    {
        base.Start();

        for (int i = 0; i < inventoryModel.Capacity; i++)
        {
            AddSlotPresenter();
        }

        inputService.OnInventory(Show, Hide);
        eventHolder.OnEndGame.Subscribe(_ =>
        {
            inputService.OnInventory(Show, Hide, false);
        });
    }

    private void OnEnable()
    {
        inventoryModel.OnAddedEvent += OnAdded;
        inventoryModel.OnRemovedEvent += OnRemoved;
    }
    private void OnDisable()
    {
        inventoryModel.OnAddedEvent -= OnAdded;
        inventoryModel.OnRemovedEvent -= OnRemoved;
    }

    public override void Show()
    {
        canvas.enabled = true;
        base.Show();
    }
    public override void Hide()
    {
        canvas.enabled = false;
        base.Hide();
    }

    private void OnRemoved(IInventoryItemModel<Weapon> itemModel, int slotIndex)
    {
        slots[slotIndex].gameObject.SetActive(false);
    }
    private void OnAdded(IInventoryItemModel<Weapon> itemModel, int slotIndex)
    {
        slots[slotIndex].Initialize(itemModel, Drop, Equip);
        slots[slotIndex].gameObject.SetActive(true);
    }

    private void Drop(IInventoryItemModel<Weapon> itemModel)
    {
        inventoryModel.TryPullOut(out Weapon _, itemModel.Component);
    }
    private void Equip(IInventoryItemModel<Weapon> itemModel)
    {
        weaponController.SetWeapon(itemModel.Component);
    }

    private void AddSlotPresenter()
    {
        if(PoolSystem.Instance.TryGet(out InventorySlotPresenter slot, false))
        {
            slots.Add(slot);
            slot.transform.SetParent(content);
        }
    }
}

