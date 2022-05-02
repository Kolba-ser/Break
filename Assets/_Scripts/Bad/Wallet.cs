using TMPro;
using UnityEngine;

public sealed class Wallet : MonoSingleton<Wallet>
{
    [SerializeField] private TextMeshProUGUI moneyContent;
    [SerializeField] private float capacity = 1000;

    private int money;

    private void Start()
    {
        EventHolder.Instance.OnEnemyDie.Subscribe(value => TryAdd(value));
    }

    public bool TryGet(int amount)
    {
        if (amount > money)
            return false;

        money -= amount;
        UpdateUI();
        return true;
    }

    public bool TryAdd(int amount)
    {
        if (money + amount > capacity)
            return false;

        money += amount;
        UpdateUI();
        return true;
    }

    private void UpdateUI()
    {
        moneyContent.text = money.ToString();
    }

}

