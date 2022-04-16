
using Break.Factories;
using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Break.Pool
{
    public sealed class PoolSystem : MonoSingleton<PoolSystem>
    {

        [SerializeField] private List<Pool> pools;

        private void Awake()
        {
            foreach (var pool in pools)
            {
                pool.Init(transform);
            }
        }

        public bool TryGet<T>(out T pooledObject, bool activate = true)
        {
            pooledObject = default;
            var targetType = typeof(T);

            var targetPool = pools.Find(_ => _.Type == targetType);

            if (targetPool != null)
            {
                pooledObject = targetPool.Get(activate).transform.GetComponent<T>();
                return true;
            }

            return false;
        }

        public bool TryRemove<T>(IPooledObject pooledObject)
        {
            var targetType = typeof(T);
            var targetPool = pools.Find(_ => _.Type == targetType);
            if (targetPool != null && pooledObject != null)
            {
                return targetPool.TryRemove(pooledObject);
            }

            return false;
        }

        public void CreatePool(int quantity, PoolFactory factory)
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

        public bool CanGet<T>()
        {
            return pools.Find(_ => _.Type == typeof(T)).isAvaliable;
        }

        [Serializable]
        private class Pool : IDisposable
        {
            [Range(1, 150)]
            public int Quantity;
            public PoolFactory factory;

            public Type Type => factory.ProductType;

            private Queue<IPooledObject> avaliables;
            private List<IPooledObject> unavaliables;
            private Transform container;

            private int numOfAvaliables;
            private int numOfUnavaliables;

            private bool isInitialized;
            private bool inProcessing;

            private IDisposable creation;
            private IDisposable destruction;

            public bool isNotUsed => numOfUnavaliables == 0;
            public bool isAvaliable => numOfAvaliables > 0;

            public void Init(Transform parent)
            {
                if (isInitialized)
                    return;

                avaliables = new Queue<IPooledObject>(Quantity);
                unavaliables = new List<IPooledObject>(Quantity);

                var go = new GameObject();
                go.name = Type.ToString();
                go.transform.SetParent(parent);
                container = go.transform;

                numOfAvaliables = Quantity;

                for (int i = 0; i < Quantity; i++)
                {
                    if (factory.TryCreate(out IPooledObject pooledObject, container.transform))
                    {
                        pooledObject.transform.gameObject.SetActive(false);
                        avaliables.Enqueue(pooledObject);
                    }
                }

                isInitialized = true;
            }

            public void InitAsync(Transform parent, float interval = 0.2f)
            {
                if (isInitialized || inProcessing)
                    return;

                inProcessing = true;

                avaliables = new Queue<IPooledObject>(Quantity);
                unavaliables = new List<IPooledObject>(Quantity);

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
                        if (factory.TryCreate(out IPooledObject pooledObject, container.transform))
                        {
                            pooledObject.transform.gameObject.SetActive(false);
                            avaliables.Enqueue(pooledObject);
                            
                            numberOfCreatedObjects++;
                            numOfAvaliables++;
                            avaliables.Enqueue(pooledObject);
                        }

                    });
            }

            public IPooledObject Get(bool activate)
            {
                var pooledObject = avaliables.Count > 0
                                    ? avaliables.Dequeue()
                                    : CreateAnother();
                unavaliables.Add(pooledObject);

                pooledObject.transform.gameObject.SetActive(activate);
                pooledObject.OnPullOut();

                numOfAvaliables--;
                numOfUnavaliables++;
                pooledObject.transform.SetParent(null);

                return pooledObject;
            }

            public bool TryRemove(IPooledObject pooledObject)
            {
                if (pooledObject != null && !unavaliables.Remove(pooledObject))
                {
                    //Debug.LogError($"{pooledObject.name} does not exists in the pool <{Type}>");
                    return false;
                }

                pooledObject.transform.SetParent(container);
                pooledObject.transform.gameObject.SetActive(false);
                pooledObject.OnPullIn();

                numOfAvaliables++;
                numOfUnavaliables--;

                avaliables.Enqueue(pooledObject);
                return true;
            }

            private IPooledObject CreateAnother()
            {
                if (factory.TryCreate(out IPooledObject pooledObject, container.transform))
                {
                    pooledObject.transform.gameObject.SetActive(false);
                    pooledObject.OnPullIn();
                    Quantity++;
                    return pooledObject;
                }

                return null;
            }

            public void Clear()
            {
                while (numOfAvaliables > 0)
                {
                    Destroy(avaliables.Dequeue().transform);
                    Quantity--;
                    numOfAvaliables--;
                }
                while (numOfUnavaliables > 0)
                {
                    Destroy(unavaliables[numOfUnavaliables - 1].transform);
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
                        if (numOfAvaliables > 0)
                        {
                            Destroy(avaliables.Dequeue().transform);
                            numOfAvaliables--;
                            Quantity--;
                        }
                        if (numOfUnavaliables > 0)
                        {
                            Destroy(unavaliables[numOfUnavaliables - 1].transform);
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
