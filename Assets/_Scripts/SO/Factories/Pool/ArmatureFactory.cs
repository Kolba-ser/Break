using Break.Projectiles;
using System;
using UnityEngine;

namespace Break.Factories
{
    [CreateAssetMenu(fileName ="Factory/ArmatureFactory")]
    public sealed class ArmatureFactory : PoolFactory
    {
        [SerializeField] private Armature armature;

        public override Type ProductType => typeof(Armature);

        public override bool TryCreate(out IPooledObject pooledObject, Transform parent = null)
        {
            pooledObject = null;

            if (verified || Verify(armature.transform))
            {
                pooledObject = DiRef.Container.InstantiatePrefab(armature, parent).GetComponent<IPooledObject>();
                return true;
            }

            return false;

        }
    }
}
