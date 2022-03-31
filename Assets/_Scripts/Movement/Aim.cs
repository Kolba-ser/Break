using Break.Weapons;
using System;
using UniRx;
using UnityEngine;


public sealed class Aim : MonoBehaviour
{
    [SerializeField] private bool rotateOnStart;
    [SerializeField] private LayerMask targetLayers;

    private Camera mainCamera;

    private Weapon activeWeapon;
    public Weapon Weapon
    {
        get { return activeWeapon; }
        set
        {
            if (value != null)
            {
                LookAtMouse();
            }
            else
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
        .TakeWhile(_ => activeWeapon != null)
        .Subscribe(_ =>
        {
            var ray = mainCamera.ScreenPointToRay(InputController.Instance.MousePosition);
            if (Physics.Raycast(ray, out var hit, targetLayers))
            {
                activeWeapon.transform.LookAt(hit.point);
            }
        });
    }
    public void LookAtTarget(Transform target)
    {
        rotation?.Dispose();

        rotation = Observable.EveryFixedUpdate()
        .TakeUntilDisable(gameObject)
        .TakeUntilDisable(target)
        .TakeWhile(_ => activeWeapon != null)
        .Subscribe(_ =>
        {
            activeWeapon.transform.LookAt(target);
        });

    }


}

