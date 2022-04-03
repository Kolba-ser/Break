using Break.Inventory;
using Break.Weapons;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotPresenter : MonoBehaviour
{
    [SerializeField] private Button dropBTN;
    [SerializeField] private Button equipBTN;
    [SerializeField] private TextMeshProUGUI name;

    public void Initialize(IInventoryItemModel<Weapon> itemModel, Action<IInventoryItemModel<Weapon>> onDrop, Action<IInventoryItemModel<Weapon>> onEquip)
    {
        dropBTN.onClick.AddListener(() => onDrop(itemModel));
        equipBTN.onClick.AddListener(() => onEquip(itemModel));
        name.text = itemModel.Info.name;
    }

    private void OnDisable()
    {
        dropBTN.onClick.RemoveAllListeners();
        name.text = "";
    }

}

