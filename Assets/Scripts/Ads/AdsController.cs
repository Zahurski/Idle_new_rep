using IdleTycoon.Configs;
using IdleTycoon.Meta;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace IdleTycoon.Ads
{
    public class AdsController : MonoBehaviour
    {
        [SerializeField] private Button button;

        private RewardedAdsButton ads;
        private bool active = false;

        private MetaValues metaValues;
        private EconomicConfig economicConfig;

        [Inject]
        private void Init(MetaValues metaValues, EconomicConfig economicConfig)
        {
            this.economicConfig = economicConfig;
            this.metaValues = metaValues;
        }

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
            metaValues.SoftMoneyCoefficient = economicConfig.AdsFactorSoftMoney;
            button.interactable = false;
            yield return new WaitForSeconds(60);
            metaValues.SoftMoneyCoefficient = economicConfig.DefaultFactorSoftMoney;
            button.interactable = true;
            active = false;
        }
    }
}