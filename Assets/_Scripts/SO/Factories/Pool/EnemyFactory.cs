


using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyFactory")]
public sealed class EnemyFactory : PoolFactory
{
    [SerializeField] private Enemy enemy;

    public override Type ProductType => typeof(Enemy);

    public override bool TryCreate(out IPooledObject pooledObject, Transform parent = null)
    {
        pooledObject = null;

        if (verified || Verify(enemy.transform))
        {
            pooledObject = DiRef.Container.InstantiatePrefab(enemy, parent).GetComponent<IPooledObject>();
            return true;
        }

        return false;
    }
}

