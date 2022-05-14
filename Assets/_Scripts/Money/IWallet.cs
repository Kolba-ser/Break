
namespace Break.Money
{
    interface IWallet
    {
        public int CurrentAmount { get; }
        public bool TryGet(int amount);
        public bool TryAdd(int amount);
    }
}
