using Break.Pause;
using Break.Weapons;
using System;
using UniRx;
using UnityEngine;
using Zenject;

public sealed class Aim : MonoBehaviour, IDisposable
{
    [SerializeField] private bool rotateOnStart;

    [Inject] private InputService inputService;
    [Inject] private EventHolder eventHolder;
    [Inject] private IPauseService pauseService;

    private Camera mainCamera;
    private Weapon activeWeapon;

    private readonly float rayDistance = 300f;

    private bool isPaused => pauseService.IsPaused;

    public Weapon Weapon
    {
        get
        {
            return activeWeapon;
        }
        set
        {
            if (value == null)
            {
                rotation?.Dispose();
            }

            activeWeapon = value;
        }
    }

    private IDisposable rotation;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Start()
    {
        if (rotateOnStart)
            LookAtMouse();

        eventHolder.OnLevelChanged.Subscribe(Dispose);
    }

    public void LookAtMouse()
    {
        rotation?.Dispose();

        rotation = Observable.EveryFixedUpdate()
        .TakeUntilDisable(gameObject)
        .TakeUntilDisable(activeWeapon)
        .Subscribe(_ =>
        {
            var ray = mainCamera.ScreenPointToRay(inputService.MousePosition);
            if (Physics.Raycast(ray, out var hit, rayDistance))
            {
                activeWeapon.Direct(hit.point);
            }
        });
    }
    public void LookAtTarget(Transform target)
    {
        rotation?.Dispose();

        rotation = Observable.EveryFixedUpdate()
        .TakeUntilDisable(gameObject)
        .TakeUntilDisable(target)
        .TakeUntilDisable(activeWeapon)
        .Subscribe(_ =>
        {
            activeWeapon.Direct(target);
        });

    }

    public void Dispose()
    {
        rotation?.Dispose();
    }

}

