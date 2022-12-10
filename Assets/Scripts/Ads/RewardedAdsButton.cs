using System;
using UnityEngine;
using UnityEngine.Advertisements;

namespace Ads
{
    public class RewardedAdsButton : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
    {
        private static string ANDROID_AD_UNIT_ID = "Rewarded_Android";
        private static string IOS_AD_UNIT_ID = "Rewarded_iOS";
        private string _adUnitId = null; // This will remain null for unsupported platforms

        public event Action RewardedAdsShowComplete;
        private void Awake()
        {
            // Get the Ad Unit ID for the current platform:
            _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
                ? IOS_AD_UNIT_ID
                : ANDROID_AD_UNIT_ID;
        }

        // Load content to the Ad Unit:
        public void LoadAd()
        {
            // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
            Debug.Log("Loading Ad: " + _adUnitId);
            Advertisement.Load(_adUnitId, this);
        }

        // If the ad successfully loads, add a listener to the button and enable it:
        public void OnUnityAdsAdLoaded(string adUnitId)
        {
            Debug.Log("Ad Loaded: " + adUnitId);
        }

        // Implement a method to execute when the user clicks the button:
        public void ShowAd()
        {
            // Then show the ad:
            Advertisement.Show(_adUnitId, this);
        }

        // Implement the Show Listener's OnUnityAdsShowComplete callback method to determine if the user gets a reward:
        public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
        {
            if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
            {
                Debug.Log("Unity Ads Rewarded Ad Completed");
                // Grant a reward.
                RewardedAdsShowComplete?.Invoke();
                // Load another ad:
                Advertisement.Load(_adUnitId, this);
            }
        }

        // Implement Load and Show Listener error callbacks:
        public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
        {
            Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
            // Use the error details to determine whether to try to load another ad.
        }

        public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
        {
            Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
            // Use the error details to determine whether to try to load another ad.
        }

        public void OnUnityAdsShowStart(string adUnitId)
        {
        }

        public void OnUnityAdsShowClick(string adUnitId)
        {
        }
    }
}
