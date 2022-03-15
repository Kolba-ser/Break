using Assets._Scripts.Projectiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Waepons
{
    public sealed class Railgun : Weapon
    {
        [SerializeField] private Projectile projectile;

        private Input inputActions;

        private void Awake()
        {
            inputActions.Player.Fire.performed += _ => Shoot();
        }

        protected override void Shoot()
        {
        }
    }
}
