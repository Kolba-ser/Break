
using UnityEngine;

public sealed class Layers : Singleton<Layers>
{
    [SerializeField] private LayerMask groundLayer;

    public int Ground => groundLayer;
}

