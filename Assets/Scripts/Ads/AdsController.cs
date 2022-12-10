using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Ads
{
    public class AdsController : MonoBehaviour
    {
        [SerializeField] private int adsMultiplier = 1;
        [SerializeField] private Button button;
        
        private RewardedAdsButton _ads;
        private bool _active = false;

        public int AdvMultiplier => adsMultiplier;

        private void Awake()
        {
            _ads = FindObjectOfType<RewardedAdsButton>();
        }

        private void Start()
        {
            _ads.RewardedAdsShowComplete += StartCoroutineRewarded;
        }

        public void AdsOn()
        {
            _active = true;
            _ads.ShowAd();
        }

        public void AdsOnDiamond()
        {
            //test
            GameManager.Instance.Diamond += 10;
        }

        private void StartCoroutineRewarded()
        {
            if(!_active) return;
            StartCoroutine(AdsActiveTime());
        }
        private IEnumerator AdsActiveTime()
        {
            adsMultiplier = 2;
            button.interactable = false;
            yield return new WaitForSeconds(60);
            adsMultiplier = 1;
            button.interactable = true;
            _active = false;
        }
    }
}