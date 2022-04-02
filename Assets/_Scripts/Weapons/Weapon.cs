

using System;
using UnityEngine;

namespace Break.Weapons
{
    public abstract class Weapon : MonoBehaviour, IEquipable
    {
        [SerializeField] protected Transform shotPoint;
        [SerializeField] protected float recoilForce;
        [SerializeField] protected float damage;

        private bool isPlaced;

        protected Aim aim;

        public bool isShooting {get; private set; }
        public bool isAimSet => aim != null;
        public float RecoilForce => recoilForce;

        public void Put(Transform parent)
        {
            if (isPlaced)
                return;

            transform.SetParent(parent);
            transform.position = parent.position;
            isPlaced = true;
        }

        public void PutAway()
        {
            transform.SetParent(null);
            aim = null;
            isPlaced = false;
        }

        public virtual void StartShoot(Action callback = null)
        {
            isShooting = true;
        }
        public virtual void StopShoot(Action callback = null)
        {
            isShooting = false;
        }

        public void SetAim(Aim aim)
        {
            this.aim = aim;
        }

        public void RemoveAim()
        {
            aim = null;
        }
    }
}
