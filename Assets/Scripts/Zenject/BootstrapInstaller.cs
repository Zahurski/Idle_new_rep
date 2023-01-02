using IdleTycoon.Ads;
using IdleTycoon.Ads.Implements;
using IdleTycoon.Meta;
using Zenject;

namespace IdleTycoon.Zenject
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<UnityRewardedAd>().AsSingle();
            Container.BindInterfacesTo<UnityBannerAd>().AsSingle();
            Container.BindInterfacesTo<UnityInterstitialAd>().AsSingle();

            Container.Bind<IAdsInitializer>().To<UnityAdsInitializer>().AsSingle();

            Container.Bind<MetaValues>().AsSingle();
            Container.Bind<IMetaValues>().To<MetaValues>().FromResolve();

            Container.BindInterfacesTo<Main>().AsSingle().NonLazy();
        }
    }
}