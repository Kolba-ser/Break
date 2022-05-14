using Zenject;

namespace Break.Installers
{
    public sealed class EventHolderInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<EventHolder>().FromNew().AsSingle().NonLazy();
        }
    }
}
