using UnityEngine;
using UnityEngine.Advertisements;

namespace IdleTycoon.Ads
{
    public class InterstitialAdExample : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
    {
        private const string ANDROID_AD_UNIT_ID = "Interstitial_Android";
        private const string IOS_AD_UNIT_ID = "Interstitial_iOS";
        
        private string adUnitId;

        private void Awake()
        {
            // Get the Ad Unit ID for the current platform:
            adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
                ? IOS_AD_UNIT_ID
                : ANDROID_AD_UNIT_ID;
        }

        // Load content to the Ad Unit:
        public void LoadAd()
        {
            // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
            Debug.Log("Loading Ad: " + adUnitId);
            Advertisement.Load(adUnitId, this);
        }

        // Show the loaded content in the Ad Unit:
        public void ShowAd()
        {
            // Note that if the ad content wasn't previously loaded, this method will fail
            Debug.Log("Showing Ad: " + adUnitId);
            Advertisement.Show(adUnitId, this);
        }

        // Implement Load Listener and Show Listener interface methods: 
        public void OnUnityAdsAdLoaded(string adUnitId)
        {
            // Optionally execute code if the Ad Unit successfully loads content.
        }

        public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
        {
            Debug.Log($"Error loading Ad Unit: {adUnitId} - {error.ToString()} - {message}");
            // Optionally execute code if the Ad Unit fails to load, such as attempting to try again.
        }

        public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
        {
            Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
            // Optionally execute code if the Ad Unit fails to show, such as loading another ad.
        }

        public void OnUnityAdsShowStart(string adUnitId)
        {
        }

        public void OnUnityAdsShowClick(string adUnitId)
        {
        }

        public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
        {
        }
    }
}