

using Break.Money;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace Break.UI.Money
{
    public sealed class MoneyPresenter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI moneyContent;

        private IWallet wallet;

        [Inject]
        private void Construct(IWallet moneyHolder)
        {
            wallet = moneyHolder;
            wallet.ObserveEveryValueChanged(amount => wallet.CurrentAmount)
                .TakeUntilDestroy(gameObject)
                .Subscribe(amount =>
                    moneyContent.text = amount.ToString());
        }
    }
}
