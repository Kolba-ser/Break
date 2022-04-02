using Break.Weapons;
using System;
using UnityEngine;

public sealed class WeaponRecoil : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private WeaponController weapon;

    private void OnEnable()
    {
        weapon.OnShotEvent += OnShot;
    }
    private void OnDisable()
    {
        weapon.OnShotEvent -= OnShot;
    }

    private void OnShot(float recoilForce, Vector3 direction)
    {
        rigidbody.AddForce(recoilForce * direction, ForceMode.Impulse);
    }
}

