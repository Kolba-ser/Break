

using System;
using UniRx;
using UnityEngine;

namespace Break.Weapons
{
    public abstract class Weapon : MonoBehaviour, IEquipable, IInventoryItemModel<Weapon>
    {
        [SerializeField] private WeaponInfo info;
        [SerializeField] protected Transform shotPoint;
        [Space(20)]
        [SerializeField] protected float recoilForce;
        [SerializeField] protected float damage;
        [SerializeField] protected float rotationSpeed = 5f;

        protected Aim aim;

        private const float equipDelay = 1f;

        public bool IsShooting
        {
            get; private set;
        }
        public bool IsAimSet => aim != null;
        public float RecoilForce => recoilForce;
        public bool IsActive => gameObject.activeSelf;
        public bool IsPlaced => transform.parent != null;
        public float RotationSpeed => rotationSpeed * Time.deltaTime;

        public ItemInfoBase Info => info;

        public Weapon Component => this;

        private void Awake()
        {
            if (IsPlaced)
                Put(transform.parent);
        }

        public virtual void Put(Transform parent)
        {
            if (IsPlaced)
                return;

            transform.SetParent(parent);
            transform.position = parent.position;
        }
        public virtual void PutAway()
        {
            gameObject.SetActive(true);
            
            Observable.Timer(equipDelay.InSec()).TakeUntilDisable(gameObject)
                .Subscribe(_ => transform.SetParent(null));
        }

        public virtual void StartShoot(Action<float> recoilCallback = null)
        {
            IsShooting = true;
        }
        public virtual void StopShoot(Action<float> recoilCallback = null)
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
            aim.Dispose();
            aim = null;
        }

    }
}
