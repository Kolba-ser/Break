using Zenject;
using Break.Money;
using UnityEngine;

namespace Break.Installers
{
    public sealed class WalletInstaller : MonoInstaller
    {

        public override void InstallBindings()
        {
            Container.Bind<IWallet>().To<Wallet>().FromNew().AsSingle();
        }
    }
}
