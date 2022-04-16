using Break.Weapons;
using System;
using UniRx;
using UnityEngine;


public sealed class Aim : MonoBehaviour, IDisposable
{
    [SerializeField] private bool rotateOnStart;
    [SerializeField] private LayerMask targetLayers;

    private Camera mainCamera;

    private Weapon activeWeapon;

    private bool isPaused => PauseController.Instance.IsPaused;

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
    }

    public void LookAtMouse()
    {
        rotation?.Dispose();

        rotation = Observable.EveryFixedUpdate()
        .TakeUntilDisable(gameObject)
        .TakeUntilDisable(activeWeapon)
        .Subscribe(_ =>
        {
            var ray = mainCamera.ScreenPointToRay(InputController.Instance.MousePosition);
            if (Physics.Raycast(ray, out var hit, targetLayers))
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

