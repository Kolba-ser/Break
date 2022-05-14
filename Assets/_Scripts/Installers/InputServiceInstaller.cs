using UnityEngine;
using Zenject;

namespace Break.Installers
{
    public sealed class InputServiceInstaller : MonoInstaller
    {
        [SerializeField] private InputService inputService;

        public override void InstallBindings()
        {
            Container.Bind<InputService>().FromInstance(inputService).AsSingle();
        }
    }
}
