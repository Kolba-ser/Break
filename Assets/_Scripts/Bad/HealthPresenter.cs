
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public sealed class HealthPresenter : MonoBehaviour
{
    [SerializeField] private Image healthContent;
    [SerializeField] private Damagable player;

    private void Start()
    {
        player.ObserveEveryValueChanged(_ => _.CurrentHealth)
            .Subscribe(_ =>
            {
                UpdateUI(player.CurrentHealth.InPercent(0, player.InitialHelth));
            });
    }

    private void UpdateUI(float percent)
    {
        healthContent.fillAmount = percent;
    }
}

