using Break.Projectiles;
using System;
using UnityEngine;

namespace Break.Factories
{
    [CreateAssetMenu(fileName ="Factory/ArmatureFactory")]
    public sealed class ArmatureFactory : FactoryBase
    {
        [SerializeField] private Armature armature;

        public override Type ProductType => typeof(Armature);

        public override Transform Create(Transform parent = null)
        {
            return Instantiate(armature.transform, parent);
        }
    }
}
