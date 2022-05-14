using Break.Pool;
using UniRx;
using UnityEngine;
using Zenject;

public sealed class EnemySpawner : MonoBehaviour
{
    [SerializeField] private int amount;
    [SerializeField] private float spawnInterval;
    [Space(20)]
    [SerializeField] private Transform[] spawnPoints;

    [Inject] private EventHolder eventHolder;

    private int killed;

    private void Start()
    {
        eventHolder.OnEnemyDie.Subscribe(_ =>
        {
            killed++;

            if (amount == killed)
                eventHolder.OnEndGame.Invoke(true);
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

