
using UnityEngine;

public sealed class Layers : MonoSingleton<Layers>
{
    [SerializeField] private LayerMask groundLayer;

    public int Ground => groundLayer.value;


}

