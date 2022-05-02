using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public sealed class DeathTrigger : MonoBehaviour
{

    private readonly float damage = 10000;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Damagable damagable))
        {
            damagable.GetDamage(damage);
        }
    }
}

