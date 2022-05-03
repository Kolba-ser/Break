using UnityEngine;

[CreateAssetMenu(fileName = "ShopItems")]
public class ShopItems : ScriptableObject
{
    [SerializeField] private ShopItem[] items;

    public int Amount => items.Length;
    public ShopItem[] Items => items;

}

