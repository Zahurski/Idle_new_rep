using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace IdleTycoon.Ads
{
    public class AdsController : MonoBehaviour
    {
        [SerializeField] private int adsMultiplier = 1;
        [SerializeField] private Button button;

        private RewardedAdsButton ads;
        private bool active = false;

        public int AdditionalMultiplier => adsMultiplier;

        private void Awake()
        {
            ads = FindObjectOfType<RewardedAdsButton>();
        }

        private void Start()
        {
            ads.RewardedAdsShowComplete += StartCoroutineRewarded;
        }

        public void AdsOn()
        {
            active = true;
            ads.ShowAd();
        }

        public void AdsOnDiamond()
        {
            GameManager.Instance.Diamond += 10;
        }

        private void StartCoroutineRewarded()
        {
            if (!active) return;
            StartCoroutine(AdsActiveTime());
        }

        private IEnumerator AdsActiveTime()
        {
            adsMultiplier = 2;
            button.interactable = false;
            yield return new WaitForSeconds(60);
            adsMultiplier = 1;
            button.interactable = true;
            active = false;
        }
    }
}