using Break.Pool;
using UniRx;
using UnityEngine;


public sealed class EnemySpawner : MonoBehaviour
{
    [SerializeField] private int amount;
    [SerializeField] private float spawnInterval;
    [Space(20)]
    [SerializeField] private Transform[] spawnPoints;

    private int killed;

    private void Start()
    {
        EventHolder.Instance.OnEnemyDie.Subscribe(_ =>
        {
            killed++;

            if (amount == killed)
                EventHolder.Instance.OnEndGame.Invoke(true);
        });
        Spawn();
    }

    private void Spawn()
    {
        int created = 0;
        Observable.Interval(spawnInterval.InSec())
            .TakeWhile(_ => created < amount)
            .Subscribe(_ =>
            {
                if(PoolSystem.Instance.TryGet(out Enemy enemy))
                {
                    enemy.transform.position = spawnPoints.GetRandom().position;
                    created++;
                }
            });
    }


}

