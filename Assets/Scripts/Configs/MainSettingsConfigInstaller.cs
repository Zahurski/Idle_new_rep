using IdleTycoon.Ads.Implements;
using UnityEngine;
using Zenject;

namespace IdleTycoon.Configs
{
    [CreateAssetMenu(fileName = nameof(MainSettingsConfigInstaller), menuName = "Configs/" + nameof(MainSettingsConfigInstaller))]
    public class MainSettingsConfigInstaller : ScriptableObjectInstaller<MainSettingsConfigInstaller>
    {
        [SerializeField] private GasStationConfig gasStationConfig;
        [SerializeField] private OilPumpConfig oilPumpConfig;
        [SerializeField] private EconomicConfig economicConfig;
        [SerializeField] private AdsConfig adsConfig;

        public override void InstallBindings()
        {
            Container.BindInstance(gasStationConfig);
            Container.BindInstance(oilPumpConfig);
            Container.BindInstance(economicConfig);
            Container.Bind<IUnityConfigAds>().FromInstance(adsConfig).AsSingle();
        }
    }
}