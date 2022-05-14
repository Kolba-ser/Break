
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public sealed class HealthPresenter : MonoBehaviour
{
    [SerializeField] private Image healthContent;
    
    [Inject] private Damagable player;

    private void Start()
    {
        player.ObserveEveryValueChanged(health => player.CurrentHealth)
            .TakeUntilDestroy(gameObject)
            .Subscribe(health => 
                healthContent.fillAmount = health.InPercent(0, player.InitialHelth));

    }
}

