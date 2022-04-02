using Break.Weapons;
using System;
using UnityEngine;

public sealed class WeaponRecoil : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private WeaponController weaponController;

    private void OnEnable()
    {
        weaponController.Subscribe(OnShot);
    }
    private void OnDisable()
    {
        weaponController.Unsubscribe(OnShot);
    }

    private void OnShot()
    {
        rigidbody.AddForce(weaponController.ActiveWeapon.RecoilForce * -weaponController.ActiveWeapon.transform.forward, ForceMode.Impulse);
    }
}

