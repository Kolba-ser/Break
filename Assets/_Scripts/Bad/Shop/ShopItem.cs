using Break.Weapons;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopItem")]
public sealed class ShopItem : ScriptableObject
{
    [SerializeField] private WeaponInfo info;
    [SerializeField] private int cost;
    [SerializeField] private GameObject gameObject;

    public int Cost => cost;
    public WeaponInfo Info => info;
    public GameObject GameObject => gameObject;
}

