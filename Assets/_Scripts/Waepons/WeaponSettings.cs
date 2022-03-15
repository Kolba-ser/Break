
using UnityEngine;

namespace Assets.Scripts.Waepons
{
    public enum DamageType
    {
        Common,
    }

    [CreateAssetMenu(fileName ="Weapon/Settings")]
    public sealed class WeaponSettings : ScriptableObject
    {
        [SerializeField] private float ammo;
        [SerializeField] private float damage;
        [SerializeField] private float shootPerSecond;
        [SerializeField] private DamageType type;

        public float Ammo => ammo;
        public float Damage => damage;
        public float ShootPerSecond => shootPerSecond;
        public DamageType Type => type;
    }
}
