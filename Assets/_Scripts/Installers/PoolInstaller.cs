

using Break.Pool;
using UnityEngine;
using Zenject;

namespace Break.Installers
{
    public sealed class PoolInstaller : MonoInstaller
    {
        [SerializeField] private PoolSystem poolSystem;

        public override void InstallBindings()
        {
            Container.Bind<PoolSystem>().FromInstance(poolSystem).AsSingle().NonLazy();
        }
    }
}
