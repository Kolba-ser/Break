using System;
using UniRx;
using UnityEngine;


public sealed class Aim : MonoBehaviour
{
    [SerializeField] private bool rotateOnStart;

    private Camera mainCamera;

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
        .Subscribe(_ =>
        {
            var ray = mainCamera.ScreenPointToRay(InputController.Instance.MousePosition);
            if (Physics.Raycast(ray, out var hit))
            {
                transform.LookAt(hit.point);
            }
        });
    }

    public void LookAtTarget(Transform target)
    {
        rotation?.Dispose();

        rotation = Observable.EveryFixedUpdate()
        .TakeUntilDisable(gameObject)
        .TakeUntilDisable(target)
        .Subscribe(_ =>
        {
            transform.LookAt(target);
        });

    }


}

