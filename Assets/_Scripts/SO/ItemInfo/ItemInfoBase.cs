
using UnityEngine;

public abstract class ItemInfoBase : ScriptableObject
{
    [SerializeField] private string name;

    public string Name => name;
}

