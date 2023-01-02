using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace IdleTycoon.Ads.Implements
{
    public class UnityInterstitialAd : IInterstitialAd
    {
        private IUnityConfigAds config;
        private UnityVideoAd videoAd;

        private string AdUnitId => (Application.platform == RuntimePlatform.IPhonePlayer)
            ? config.IOSInterstitialUnitId
            : config.AndroidInterstitialUnitId;

        [Inject]
        private void Init(IUnityConfigAds configAds)
        {
            this.config = configAds;
            videoAd = new UnityVideoAd(config, AdUnitId);
        }

        public void Show()
        {
            videoAd.Show();
        }

        public async UniTask<AdShowResultEnum> ShowAsync()
        {
            return await videoAd.ShowAsync();
        }

        public void Load()
        {
            videoAd.Load();
        }
    }
}