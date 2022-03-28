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

    private void OnShot(float recoilForce)
    {
        rigidbody.AddForce(recoilForce * -transform.forward, ForceMode.Impulse);
    }
}

