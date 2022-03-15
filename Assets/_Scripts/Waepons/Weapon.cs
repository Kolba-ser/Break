

using UnityEngine;

namespace Assets.Scripts.Waepons
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] protected WeaponSettings settings;


        protected abstract void Shoot();
    }
}
