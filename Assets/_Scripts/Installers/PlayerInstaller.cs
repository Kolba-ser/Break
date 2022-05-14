using UnityEngine;
using Zenject;

namespace Break.Installers
{
    public sealed class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private Player player;

        public override void InstallBindings()
        {
            Container.Bind<Damagable>().FromInstance(player).AsSingle();
            Container.Bind<Player>().FromInstance(player).AsSingle();
        }
    }
}
