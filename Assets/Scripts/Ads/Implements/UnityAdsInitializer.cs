using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Advertisements;
using Zenject;

namespace IdleTycoon.Ads.Implements
{
    public class UnityAdsInitializer : IAdsInitializer, IUnityAdsInitializationListener
    {
        private IUnityConfigAds adsConfig;
        private List<IAd> ads;

        public bool IsInitialized => Advertisement.isInitialized;

        private string GameId => (Application.platform == RuntimePlatform.IPhonePlayer)
            ? adsConfig.IOSGameID
            : adsConfig.AndroidGameID;

        [Inject]
        private void Init(IUnityConfigAds adsConfig, List<IAd> ads)
        {
            this.ads = ads;
            this.adsConfig = adsConfig;
        }

        public void Initialize()
        {
            Advertisement.Initialize(GameId, adsConfig.IsTestMode, this);

            if (adsConfig.IsTestMode)
            {
                OnInitializationComplete();
            }
        }

        public async UniTask WaitInitialize(CancellationToken cancellationToken)
        {
            await UniTask.WaitUntil(() => IsInitialized, PlayerLoopTiming.Update, cancellationToken);
        }

        public void OnInitializationComplete()
        {
            if (adsConfig.IsDebugMode)
            {
                Debug.Log("Ads initialization complete");
            }

            foreach (var ad in ads)
            {
                ad.Load();
            }
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            if (adsConfig.IsDebugMode)
            {
                Debug.LogError("Unable to init ads, error: " + error + " message: " + message);
            }
        }
    }
}