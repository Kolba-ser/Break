using Break.Units;
using System;
using UnityEngine;

namespace Assets._Scripts.SO.Factories.Pool
{
    [CreateAssetMenu(fileName = "VfxFactory")]
    class VfxFactory : PoolFactory
    {
        [SerializeField] private Vfx vfx;

        public override Type ProductType => typeof(Vfx);

        public override bool TryCreate(out IPooledObject pooledObject, Transform parent = null)
        {
            pooledObject = null;

            if (verified || Verify(vfx.transform))
            {
                pooledObject = DiRef.Container.InstantiatePrefab(vfx, parent).GetComponent<IPooledObject>();
                return true;
            }

            return false;
        }
    }
}
