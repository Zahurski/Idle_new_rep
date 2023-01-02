using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace IdleTycoon.Ads.Implements
{
    public class UnityRewardedAd : IRewardedAd
    {
        private IUnityConfigAds config;
        private UnityVideoAd videoAd;

        private string AdUnitId => (Application.platform == RuntimePlatform.IPhonePlayer)
            ? config.IOSRewardedUnitId
            : config.AndroidRewardedUnitId;

        [Inject]
        private void Init(IUnityConfigAds configAds)
        {
            this.config = configAds;
            videoAd = new UnityVideoAd(config, AdUnitId);
        }

        public void Load()
        {
            videoAd.Load();
        }

        public async UniTask<AdShowResultEnum> ShowAsync()
        {
            return await videoAd.ShowAsync();
        }
    }
}