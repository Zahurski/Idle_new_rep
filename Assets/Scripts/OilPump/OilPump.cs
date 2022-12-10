using System;
using System.Collections;
using Ads;
using OilPump.Config;
using UnityEngine;

namespace OilPump
{
    public class OilPump : MonoBehaviour
    {
        [SerializeField] private OilPumpConfig config;
        private bool _complete;
        private AdsController _ads;
        private OilPumpMoneyIncreaseText _oilPumpMoneyIncreaseText;
        private OilPumpLoading _oilPumpLoading;
        private void Awake()
        {
            _ads = FindObjectOfType<AdsController>();
            _oilPumpMoneyIncreaseText = FindObjectOfType<OilPumpMoneyIncreaseText>();
            _oilPumpLoading = FindObjectOfType<OilPumpLoading>();
        }

        private void Start()
        {
            StartCoroutine(Pumping());
        }

        private IEnumerator Pumping()
        {
            while (true)
            {
                _oilPumpLoading.IsActive = true;
                yield return new WaitForSeconds(config.PumpingTime);
                _oilPumpLoading.IsActive = false;
                _oilPumpMoneyIncreaseText.Pump = true;
                GameManager.Instance.Money += config.Cost * _ads.AdvMultiplier;
                yield return new WaitForSeconds(0.01f);
            }
        }
    }
}