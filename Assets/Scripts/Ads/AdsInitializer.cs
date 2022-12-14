using UnityEngine;
using UnityEngine.Advertisements;

namespace IdleTycoon.Ads
{
    public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
    {
        private const string ANDROID_GAME_ID = "4944531";
        private const string IOS_GAME_ID = "4944530";

        [SerializeField] private bool _testMode = true;

        private InterstitialAdExample _interstitial;
        private RewardedAdsButton _rewarded;
        private BannerAdExample _banner;
        private string _gameId;

        private void Awake()
        {
            InitializeAds();
            _interstitial = FindObjectOfType<InterstitialAdExample>();
            _rewarded = FindObjectOfType<RewardedAdsButton>();
            _banner = FindObjectOfType<BannerAdExample>();
        }

        private void InitializeAds()
        {
            _gameId = (Application.platform == RuntimePlatform.IPhonePlayer)
                ? IOS_GAME_ID
                : ANDROID_GAME_ID;
            Advertisement.Initialize(_gameId, _testMode, this);
        }

        public void OnInitializationComplete()
        {
            Debug.Log("Unity Ads initialization complete.");
            _interstitial.LoadAd();
            _rewarded.LoadAd();
            _banner.LoadBanner();
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
        }
    }
}