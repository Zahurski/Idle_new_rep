using System;
using UnityEngine;
using UnityEngine.Advertisements;
using Zenject;

namespace IdleTycoon.Ads.Implements
{
    public class UnityBannerAd : IBannerAd
    {
        private IUnityConfigAds config;

        private string AdUnitId => (Application.platform == RuntimePlatform.IPhonePlayer)
            ? config.IOSBannerUnitId
            : config.AndroidBannerUnitId;

        [Inject]
        private void Init(IUnityConfigAds configAds)
        {
            config = configAds;
        }

        public void Load()
        {
            Advertisement.Banner.SetPosition(ConvertToBannerPosition(config.BannerPosition));

            BannerLoadOptions options = new BannerLoadOptions
            {
                loadCallback = OnBannerLoaded,
                errorCallback = OnBannerError
            };

            // Load the Ad Unit with banner content:
            Advertisement.Banner.Load(AdUnitId, options);
        }

        public void Show()
        {
            // Set up options to notify the SDK of show events:
            BannerOptions options = new BannerOptions
            {
                clickCallback = OnBannerClicked,
                hideCallback = OnBannerHidden,
                showCallback = OnBannerShown
            };

            // Show the loaded Banner Ad Unit:
            Advertisement.Banner.Show(AdUnitId, options);
        }

        public void Hide()
        {
            Advertisement.Banner.Hide();
        }

        private void OnBannerLoaded()
        {
            if (config.IsDebugMode)
            {
                Debug.Log("Banner loaded");
            }
        }

        private void OnBannerError(string message)
        {
            if (config.IsDebugMode)
            {
                Debug.Log($"Banner Error: {message}");
            }
        }

        private void OnBannerClicked()
        {
            if (config.IsDebugMode)
            {
                Debug.Log($"Banner:OnBannerClicked");
            }
        }

        private void OnBannerShown()
        {
            if (config.IsDebugMode)
            {
                Debug.Log($"Banner:OnBannerShown");
            }
        }

        private void OnBannerHidden()
        {
            if (config.IsDebugMode)
            {
                Debug.Log($"Banner:OnBannerHidden");
            }
        }


        private BannerPosition ConvertToBannerPosition(AdsBannerPosition adsBannerPosition)
        {
            switch (adsBannerPosition)
            {
                case AdsBannerPosition.TopLeft:
                    return BannerPosition.TOP_LEFT;
                case AdsBannerPosition.TopCenter:
                    return BannerPosition.TOP_CENTER;
                case AdsBannerPosition.TopRight:
                    return BannerPosition.TOP_RIGHT;
                case AdsBannerPosition.BottomLeft:
                    return BannerPosition.BOTTOM_LEFT;
                case AdsBannerPosition.BottomCenter:
                    return BannerPosition.BOTTOM_CENTER;
                case AdsBannerPosition.BottomRight:
                    return BannerPosition.BOTTOM_RIGHT;
                case AdsBannerPosition.Center:
                    return BannerPosition.BOTTOM_CENTER;
                default:
                    throw new ArgumentOutOfRangeException(nameof(adsBannerPosition), adsBannerPosition, null);
            }
        }
    }
}