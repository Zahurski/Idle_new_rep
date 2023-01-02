using IdleTycoon.Ads;
using IdleTycoon.Configs;
using IdleTycoon.Meta;
using Zenject;

namespace IdleTycoon
{
    public class Main : IInitializable
    {
        private IAdsInitializer adsInitializer;
        private MetaValues metaValues;
        private EconomicConfig economicConfig;

        [Inject]
        private void Init(IAdsInitializer adsInitializer, MetaValues metaValues, EconomicConfig economicConfig)
        {
            this.economicConfig = economicConfig;
            this.metaValues = metaValues;
            this.adsInitializer = adsInitializer;
        }

        public void Initialize()
        {
            //d.gankov: here we can load saved data
            adsInitializer.Initialize();
            metaValues.SoftMoneyCoefficient = economicConfig.DefaultFactorSoftMoney;
        }
    }
}