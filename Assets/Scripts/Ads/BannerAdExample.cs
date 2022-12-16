using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Serialization;

namespace IdleTycoon.Ads
{
    public class BannerAdExample : MonoBehaviour
    {
        private const string ANDROID_AD_UNIT_ID = "Banner_Android";
        private const string IOS_AD_UNIT_ID = "Banner_iOS";

        [FormerlySerializedAs("_bannerPosition")] [SerializeField]
        private BannerPosition bannerPosition = BannerPosition.BOTTOM_CENTER;

        private string adUnitId = null; // This will remain null for unsupported platforms.

        private void Awake()
        {
            // Get the Ad Unit ID for the current platform:
            adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
                ? IOS_AD_UNIT_ID
                : ANDROID_AD_UNIT_ID;

            // Set the banner position:
            Advertisement.Banner.SetPosition(bannerPosition);
        }

        // Implement a method to call when the Load Banner button is clicked:
        public void LoadBanner()
        {
            // Set up options to notify the SDK of load events:
            BannerLoadOptions options = new BannerLoadOptions
            {
                loadCallback = OnBannerLoaded,
                errorCallback = OnBannerError
            };

            // Load the Ad Unit with banner content:
            Advertisement.Banner.Load(adUnitId, options);
        }

        // Implement code to execute when the loadCallback event triggers:
        void OnBannerLoaded()
        {
            Debug.Log("Banner loaded");
        }

        // Implement code to execute when the load errorCallback event triggers:
        void OnBannerError(string message)
        {
            Debug.Log($"Banner Error: {message}");
            // Optionally execute additional code, such as attempting to load another ad.
        }

        // Implement a method to call when the Show Banner button is clicked:
        void ShowBannerAd()
        {
            // Set up options to notify the SDK of show events:
            BannerOptions options = new BannerOptions
            {
                clickCallback = OnBannerClicked,
                hideCallback = OnBannerHidden,
                showCallback = OnBannerShown
            };

            // Show the loaded Banner Ad Unit:
            Advertisement.Banner.Show(adUnitId, options);
        }

        // Implement a method to call when the Hide Banner button is clicked:
        void HideBannerAd()
        {
            // Hide the banner:
            Advertisement.Banner.Hide();
        }

        void OnBannerClicked()
        {
        }

        void OnBannerShown()
        {
        }

        void OnBannerHidden()
        {
        }
    }
}