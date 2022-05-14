using Zenject;

namespace Break.Installers
{
    public sealed class DiContainerInstaller : MonoInstaller
    {
        [Inject] private DiContainer diContainer;

        public override void InstallBindings()
        {
            DiRef.Container = diContainer;
        }
    }
}
