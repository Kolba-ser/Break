using Break.Weapons;
using UnityEngine;

public sealed class WeaponController : MonoBehaviour
{
    [SerializeField] private Weapon activeWeapon;
    [SerializeField] private Aim aim;

    public delegate void WeaponHandler(float recoilForce, Vector3 direction);
    public event WeaponHandler OnShotEvent;

    private void Start()
    {
        InputController.Instance.OnFire(OnFireStart, OnFireStop);
    }

    public void SetWeapon(Weapon weapon)
    {
        if (weapon == null)
        {
            Debug.LogError("The weapon is null");
            return;
        }
        if (activeWeapon != null)
            activeWeapon.RemoveAim();

        activeWeapon = weapon;
        activeWeapon.SetAim(aim);
        aim.Weapon = weapon;
    }

    private void OnFireStart()
    {
        if (activeWeapon == null || activeWeapon.isShooting)
            return;

        activeWeapon.StartShoot();
        OnShotEvent?.Invoke(activeWeapon.RecoilForce, -activeWeapon.transform.forward);
    }

    private void OnFireStop()
    {
        if (activeWeapon == null || !activeWeapon.isShooting)
            return;

        activeWeapon.StopShoot();
    }


}

