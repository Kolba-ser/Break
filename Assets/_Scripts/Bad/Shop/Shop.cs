
using System;
using UnityEngine;

public sealed class Shop : MonoBehaviour
{

    [SerializeField] private Transform content;
    [SerializeField] private ShopItems shopItems;
    [SerializeField] private ShopSlot slot;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Canvas shop;

    private Collider player;

    private void Awake()
    {
        for (int i = 0; i < shopItems.Amount; i++)
        {
            var created = Instantiate(slot, content);
            var item = shopItems.Items[i];
            created.Initialize(item.Info.Name, () => Buy(item));
        }
    }

    private void Buy(ShopItem item)
    {
        if (Wallet.Instance.TryGet(item.Cost))
        {
            Instantiate(item.GameObject, spawnPoint.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            shop.enabled = true;
            player = other;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other == player)
        {
            shop.enabled = false;
        }
    }

}

