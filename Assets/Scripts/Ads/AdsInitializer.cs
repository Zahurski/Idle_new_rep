using UnityEngine;
using UnityEngine.Advertisements;

namespace Ads
{
    using UnityEngine;
    using UnityEngine.Advertisements;
 
    public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
    {
        [SerializeField] private bool _testMode = true;
        private static string ANDROID_GAME_ID = "4944531";
        private static string IOS_GAME_ID = "4944530";
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