using UnityEngine;
using Zenject;

namespace Break.Money
{
    /// <summary>
    /// Для отслеживания изменения баланса<br>
    /// лучше использовать Class.ObserveEveryValueChanged
    /// </summary>
    public sealed class Wallet : IWallet
    {
        [SerializeField] private float capacity = 1000;

        private EventHolder eventHolder;

        private int currentAmount;

        public int CurrentAmount => currentAmount;

        [Inject]
        private void Construct(EventHolder eventHolder)
        {
            this.eventHolder = eventHolder;
            this.eventHolder.OnEnemyDie.Subscribe(value => TryAdd(value));
        }

        public bool TryGet(int amount)
        {
            if (amount > currentAmount)
                return false;

            currentAmount -= amount;
            return true;
        }

        public bool TryAdd(int amount)
        {
            Debug.Log("Денюшки добавлены");
            if (currentAmount + amount > capacity)
                return false;

            currentAmount += amount;
            return true;
        }
    }
}

