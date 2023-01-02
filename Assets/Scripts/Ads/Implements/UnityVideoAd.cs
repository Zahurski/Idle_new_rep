using Cysharp.Threading.Tasks;
using IdleTycoon.Ads.Exceptions;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.Advertisements;

namespace IdleTycoon.Ads.Implements
{
    internal class UnityVideoAd : IUnityAdsLoadListener, IUnityAdsShowListener
    {
        private readonly IUnityConfigAds config;
        private readonly string adUnitId;

        private AdShowResultEnum result;
        private CancellationTokenSource ctsShowing;

        public UnityVideoAd(IUnityConfigAds config, string adUnitId)
        {
            this.adUnitId = adUnitId;
            this.config = config;
        }

        public void Load()
        {
            if (config.IsDebugMode)
            {
                Debug.Log("Loading Ad: " + adUnitId);
            }

            Advertisement.Load(adUnitId, this);
        }

        public void Show()
        {
            Advertisement.Show(adUnitId, this);
        }

        public async UniTask<AdShowResultEnum> ShowAsync()
        {
            if (!Advertisement.isInitialized)
            {
                throw new NotInitializeSDK($"Must be check on initialize the sdk thought {nameof(IAdsInitializer)}.{nameof(IAdsInitializer.IsInitialized)}");
            }

            ctsShowing = new CancellationTokenSource();

            Advertisement.Show(adUnitId, this);
            await UniTask.WaitUntilCanceled(ctsShowing.Token);
            return result;
        }

        void IUnityAdsLoadListener.OnUnityAdsAdLoaded(string adUnitId)
        {
            if (config.IsDebugMode)
            {
                Debug.Log("Ad Loaded: " + adUnitId);
            }
        }

        void IUnityAdsLoadListener.OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
        {
            if (config.IsDebugMode)
            {
                Debug.Log($"Error loading Ad Unit: {adUnitId} - {error.ToString()} - {message}");
            }
        }

        void IUnityAdsShowListener.OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
            if (config.IsDebugMode)
            {
                Debug.LogError("Unable to show ads: " + placementId + " error: " + error + " message: " + message);
            }

            result = AdShowResultEnum.Failed;
            ctsShowing.Cancel();
        }

        void IUnityAdsShowListener.OnUnityAdsShowStart(string placementId)
        {
            if (config.IsDebugMode)
            {
                Debug.Log("OnUnityAdsShowStart: " + placementId);
            }
        }

        void IUnityAdsShowListener.OnUnityAdsShowClick(string placementId)
        {
            if (config.IsDebugMode)
            {
                Debug.Log("OnUnityAdsShowClick: " + placementId);
            }
        }

        void IUnityAdsShowListener.OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            if (config.IsDebugMode)
            {
                Debug.Log("OnUnityAdsShowComplete: " + placementId + " state: " + showCompletionState);
            }

            if (placementId == adUnitId)
            {
                switch (showCompletionState)
                {
                    case UnityAdsShowCompletionState.SKIPPED:
                        result = AdShowResultEnum.Skipped;
                        break;
                    case UnityAdsShowCompletionState.COMPLETED:
                        result = AdShowResultEnum.Completed;
                        break;
                    case UnityAdsShowCompletionState.UNKNOWN:
                        result = AdShowResultEnum.Unknown;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(showCompletionState), showCompletionState, null);
                }
            }

            ctsShowing.Cancel();
        }
    }
}