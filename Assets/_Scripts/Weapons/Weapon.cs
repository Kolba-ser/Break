

using UnityEngine;

namespace Assets.Scripts.Waepons
{
    public abstract class Weapon : MonoBehaviour
    {
        protected bool isShooting {get; private set; }

        protected virtual void StartShoot()
        {
            isShooting = true;
        }
        protected virtual void StopShoot()
        {
            isShooting = false;
        }
    }
}
