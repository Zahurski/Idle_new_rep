using IdleTycoon.Ads;
using IdleTycoon.Ads.Implements;
using UnityEngine;

namespace IdleTycoon.Configs
{
    [CreateAssetMenu(fileName = nameof(AdsConfig), menuName = "Configs/" + nameof(AdsConfig))]
    public class AdsConfig : ScriptableObject, IUnityConfigAds
    {
        [SerializeField] private bool isTestMode;
        [SerializeField] private bool isDebugMode;
        [SerializeField] private string androidGameID = "4944531";
        [SerializeField] private string iOSGameID = "4944530";

        [SerializeField] private float durationTemporarySoftMoneyUp;
        [SerializeField] private float factorTemporarySoftMoneyUp;
        [SerializeField] private string androidBannerUnitId = "Banner_Android";
        [SerializeField] private string iosBannerUnitId = "Banner_iOS";
        [SerializeField] private AdsBannerPosition bannerPosition = AdsBannerPosition.BottomCenter;
        [SerializeField] private string androidInterstitialUnitId = "Interstitial_Android";
        [SerializeField] private string iosInterstitialUnitId = "Interstitial_iOS";
        [SerializeField] private string androidRewardedUnitId = "Rewarded_Android";
        [SerializeField] private string iosRewardedUnitId = "Rewarded_iOS";

        public float DurationTemporarySoftMoneyUp => durationTemporarySoftMoneyUp;
        public float FactorTemporarySoftMoneyUp => factorTemporarySoftMoneyUp;
        public bool IsDebugMode => isDebugMode;
        public string AndroidBannerUnitId => androidBannerUnitId;
        public string IOSBannerUnitId => iosBannerUnitId;
        public AdsBannerPosition BannerPosition => bannerPosition;
        public string AndroidInterstitialUnitId => androidInterstitialUnitId;
        public string IOSInterstitialUnitId => iosInterstitialUnitId;
        public string AndroidRewardedUnitId => androidRewardedUnitId;
        public string IOSRewardedUnitId => iosRewardedUnitId;
        public bool IsTestMode => isTestMode;
        public string AndroidGameID => androidGameID;
        public string IOSGameID => iOSGameID;
    }
}