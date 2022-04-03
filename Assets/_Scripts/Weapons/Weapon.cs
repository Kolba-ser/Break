

using System;
using UniRx;
using UnityEngine;

namespace Break.Weapons
{
    public abstract class Weapon : MonoBehaviour, IEquipable, IInventoryItemModel<Weapon>
    {
        [SerializeField] private WeaponInfo info;
        [SerializeField] protected Transform shotPoint;
        [SerializeField] protected float recoilForce;
        [SerializeField] protected float damage;

        protected Aim aim;

        private bool isPlaced;

        private const float equipDelay = 1f;

        public bool IsShooting
        {
            get; private set;
        }
        public bool IsAimSet => aim != null;
        public float RecoilForce => recoilForce;
        public bool IsActive => gameObject.activeSelf;
        bool IEquipable.IsPlaced => isPlaced;

        public ItemInfoBase Info => info;

        public Weapon Component => this;


        public virtual void Put(Transform parent)
        {
            if (isPlaced)
                return;

            isPlaced = true;
            transform.SetParent(parent);
            transform.position = parent.position;
        }
        public virtual void PutAway()
        {
            gameObject.SetActive(true);
            transform.SetParent(null);

            Observable.Timer(equipDelay.InSec()).TakeUntilDisable(gameObject)
                .Subscribe(_ => isPlaced = false);
        }

        public virtual void StartShoot(Action callback = null)
        {
            IsShooting = true;
        }
        public virtual void StopShoot(Action callback = null)
        {
            IsShooting = false;
        }

        public virtual void Activate()
        {
            gameObject.SetActive(true);
        }
        public virtual void Deactivate()
        {
            gameObject.SetActive(false);
        }

        public void SetAim(Aim aim)
        {
            this.aim = aim;
        }
        public void RemoveAim()
        {
            aim.Deactivate();
            aim = null;
        }

    }
}
