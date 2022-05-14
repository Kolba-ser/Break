using Break.Pause;
using Zenject;

namespace Break.Installers
{
    public sealed class PauseServiceInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IPauseService>().To<PauseService>().FromNew().AsSingle();
        }
    }
}
