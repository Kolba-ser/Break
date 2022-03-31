
using Break.Factories;
using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Break.Pool
{
    public sealed class PoolSystem : Singleton<PoolSystem>
    {

        [SerializeField] private List<Pool> pools;

        private void Start()
        {
            foreach (var pool in pools)
            {
                pool.Init(transform);
            }
        }

        public bool TryGet<T>(out T pooledObject)
        {
            pooledObject = default;
            var targetType = typeof(T);

            var targetPool = pools.Find(_ => _.Type == targetType);

            if (targetPool != null)
            {
                pooledObject = targetPool.Get().GetComponent<T>();
                return true;
            }

            return false;
        }

        public bool TryRemove<T>(Transform pooledObject)
        {
            var targetType = typeof(T);
            var targetPool = pools.Find(_ => _.Type == targetType);
            if (targetPool != null && pooledObject != null)
            {
                return targetPool.TryRemove(pooledObject);
            }

            return false;
        }

        public void CreatePool(int quantity, FactoryBase factory)
        {
            var pool = new Pool();
            pool.factory = factory;
            pool.Quantity = quantity;

            pool.Init(transform);
            pools.Add(pool);
        }
        /// <summary>
        /// Удаляет пул и объекты по передоваемоему типу.
        /// Во избежание ошибок, перед тем как очистить пул, убедитесть что все объекты были возвращены !
        /// </summary>
        public void ClearPool<T>()
        {
            var targetType = typeof(T);
            var targetPool = pools.Find(_ => _.Type == targetType);

            if (targetPool != null)
            {
                targetPool.Clear();
            }
        }
        /// <summary>
        /// Удаляет пул и объекты по передоваемоему типу, с определенным инетрвалом.
        /// Во избежание ошибок, перед тем как очистить пул, убедитесть что все объекты были возвращены !
        /// </summary>
        public void ClearPoolAsync<T>()
        {
            var targetType = typeof(T);
            var targetPool = pools.Find(_ => _.Type == targetType);

            if (targetPool != null)
            {
                targetPool.ClearAsync();
            }
        }

        [Serializable]
        private class Pool : IDisposable
        {
            [Range(1, 150)]
            public int Quantity;
            public FactoryBase factory;

            public Type Type;

            private Queue<Transform> avaliables;
            private List<Transform> unavaliables;
            private Transform container;

            private int numOfAvaliables;
            private int numOfUnavaliables;

            private bool isInitialized;
            private bool inProcessing;

            private IDisposable creation;
            private IDisposable destruction;

            public bool isNotUsed => unavaliables.Count == 0;

            public void Init(Transform parent)
            {
                if (isInitialized)
                    return;

                Type = factory.ProductType;

                avaliables = new Queue<Transform>(Quantity);
                unavaliables = new List<Transform>(Quantity);

                var go = new GameObject();
                go.name = Type.ToString();
                go.transform.SetParent(parent);
                container = go.transform;

                numOfAvaliables = Quantity;

                for (int i = 0; i < Quantity; i++)
                {
                    var created = factory.Create(container.transform);
                    created.gameObject.SetActive(false);
                    avaliables.Enqueue(created);
                }

                isInitialized = true;
            }

            public void InitAsync(Transform parent, float interval = 0.2f)
            {
                if (isInitialized || inProcessing)
                    return;

                inProcessing = true;
                Type = factory.ProductType;

                avaliables = new Queue<Transform>(Quantity);
                unavaliables = new List<Transform>(Quantity);

                var go = new GameObject();
                go.name = Type.ToString();
                go.transform.SetParent(parent);
                container = go.transform;

                int numberOfCreatedObjects = 0;

                creation = Observable.Interval(interval.InSec()).TakeWhile(_ => numberOfCreatedObjects < Quantity)
                    .Finally(() =>
                    {
                        inProcessing = false;
                        isInitialized = true;
                    })
                    .Subscribe(_ =>
                    {
                        var createdObject = factory.Create();
                        createdObject.SetParent(container);
                        createdObject.gameObject.SetActive(false);

                        numberOfCreatedObjects++;
                        numOfAvaliables++;
                        avaliables.Enqueue(createdObject);
                    });
            }

            public Transform Get()
            {
                var pooledObject = avaliables.Count > 0
                                    ? avaliables.Dequeue()
                                    : CreateAnother();
                unavaliables.Add(pooledObject);

                pooledObject.gameObject.SetActive(true);

                numOfAvaliables--;
                numOfUnavaliables++;

                return pooledObject;
            }

            public bool TryRemove(Transform pooledObject)
            {
                if (!unavaliables.Remove(pooledObject))
                {
                    //Debug.LogError($"{pooledObject.name} does not exists in the pool <{Type}>");
                    return false;
                }

                pooledObject.SetParent(container);
                pooledObject.gameObject.SetActive(false);

                avaliables.Enqueue(pooledObject);
                return true;
            }

            private Transform CreateAnother()
            {

                var created = factory.Create(container.transform);
                created.gameObject.SetActive(true);
                Quantity++;
                return created;
            }

            public void Clear()
            {
                while (numOfAvaliables > 0)
                {
                    Destroy(avaliables.Dequeue());
                    Quantity--;
                    numOfAvaliables--;
                }
                while(numOfUnavaliables > 0)
                {
                    Destroy(unavaliables[numOfUnavaliables - 1]);
                    unavaliables.Remove(unavaliables[numOfUnavaliables - 1]);
                    Quantity--;
                    numOfUnavaliables--;
                }
            }

            public void ClearAsync(float interval = 0.2f)
            {
                if (inProcessing)
                    return;

                inProcessing = true;
                destruction = Observable.Interval(interval.InSec()).TakeWhile(_ => Quantity > 0)
                    .Finally(() => inProcessing = false)
                    .Subscribe(_ =>
                    {
                        if(numOfAvaliables > 0)
                        {
                            Destroy(avaliables.Dequeue());
                            numOfAvaliables--;
                            Quantity--;
                        }
                        if(numOfUnavaliables > 0)
                        {
                            Destroy(unavaliables[numOfUnavaliables - 1]);
                            unavaliables.Remove(unavaliables[numOfUnavaliables - 1]);
                            numOfUnavaliables--;
                            Quantity--;
                        }
                    });
            }

            public void Dispose()
            {
                creation?.Dispose();
                destruction.Dispose();
            }
        }
    }
}
