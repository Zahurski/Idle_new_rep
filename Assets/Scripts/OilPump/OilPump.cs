using System.Collections;
using IdleTycoon.Ads;
using IdleTycoon.OilPump.Config;
using UnityEngine;

namespace IdleTycoon.OilPump
{
    public class OilPump : MonoBehaviour
    {
        [SerializeField] private OilPumpConfig config;

        private bool complete;
        private AdsController ads;
        private OilPumpMoneyIncreaseText oilPumpMoneyIncreaseText;
        private OilPumpLoading oilPumpLoading;

        private void Awake()
        {
            ads = FindObjectOfType<AdsController>();
            oilPumpMoneyIncreaseText = FindObjectOfType<OilPumpMoneyIncreaseText>();
            oilPumpLoading = FindObjectOfType<OilPumpLoading>();
        }

        private void Start()
        {
            StartCoroutine(Pumping());
        }

        private IEnumerator Pumping()
        {
            while (true)
            {
                oilPumpLoading.IsActive = true;
                yield return new WaitForSeconds(config.PumpingTime);
                oilPumpLoading.IsActive = false;
                oilPumpMoneyIncreaseText.Pump = true;
                GameManager.Instance.Money += config.Cost * ads.AdvMultiplier;
                yield return new WaitForSeconds(0.01f);
            }
            yield return null;
        }
    }
}