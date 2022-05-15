using Break.Pool;
using System;
using UnityEngine;
using Zenject;

namespace Break.Units
{
    public sealed class Vfx : MonoBehaviour, IPooledObject
    {
        [SerializeField] private string name;
        [SerializeField] private ParticleSystem vfx;

        [Inject] private PoolSystem poolSystem;

        private void OnParticleSystemStopped()
        {
            Debug.Log(poolSystem.TryRemove(this, name));
        }
        public void OnPullIn()
        {
            
        }

        public void OnPullOut()
        {
            vfx.Play();
        }

    }
}
