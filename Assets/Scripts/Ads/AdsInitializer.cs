using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Serialization;

namespace IdleTycoon.Ads
{
    public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
    {
        private const string ANDROID_GAME_ID = "4944531";
        private const string IOS_GAME_ID = "4944530";

        [FormerlySerializedAs("_testMode")]
        [SerializeField] private bool testMode = true;

        private InterstitialAdExample interstitial;
        private RewardedAdsButton rewarded;
        private BannerAdExample banner;
        private string gameId;

        private void Awake()
        {
            InitializeAds();
            interstitial = FindObjectOfType<InterstitialAdExample>();
            rewarded = FindObjectOfType<RewardedAdsButton>();
            banner = FindObjectOfType<BannerAdExample>();
        }

        private void InitializeAds()
        {
            gameId = (Application.platform == RuntimePlatform.IPhonePlayer)
                ? IOS_GAME_ID
                : ANDROID_GAME_ID;
            Advertisement.Initialize(gameId, testMode, this);
        }

        public void OnInitializationComplete()
        {
            Debug.Log("Unity Ads initialization complete.");
            interstitial.LoadAd();
            rewarded.LoadAd();
            banner.LoadBanner();
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
        }
    }
}