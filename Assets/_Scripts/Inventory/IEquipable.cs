using UnityEngine;

public interface IEquipable
{
    public bool IsPlaced { get; }

    public void Put(Transform parent);

    public void PutAway();

    
}