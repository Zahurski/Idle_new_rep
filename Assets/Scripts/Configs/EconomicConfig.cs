using UnityEngine;

namespace IdleTycoon.Configs
{
    [CreateAssetMenu(fileName = nameof(EconomicConfig), menuName = "Configs/" + nameof(EconomicConfig))]
    public class EconomicConfig : ScriptableObject
    {
        [SerializeField] private float defaultFactorSoftMoney;
        [SerializeField] private float adsFactorSoftMoney;

        public float DefaultFactorSoftMoney => defaultFactorSoftMoney;
        public float AdsFactorSoftMoney => adsFactorSoftMoney;
    }
}