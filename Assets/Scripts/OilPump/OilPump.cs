using System.Collections;
using IdleTycoon.Ads;
using IdleTycoon.Configs;
using IdleTycoon.Meta;
using UnityEngine;
using Zenject;

namespace IdleTycoon.OilPump
{
    public class OilPump : MonoBehaviour
    {
        private bool complete;
        private AdsController ads;
        private OilPumpMoneyIncreaseText oilPumpMoneyIncreaseText;
        private OilPumpLoading oilPumpLoading;

        private OilPumpConfig config;
        private IMetaValues metaValues;

        [Inject]
        private void Init(OilPumpConfig config, IMetaValues metaValues)
        {
            this.metaValues = metaValues;
            this.config = config;
        }

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
                GameManager.Instance.Money += config.Cost * metaValues.SoftMoneyCoefficient;
                yield return new WaitForSeconds(0.01f);
            }
        }
    }
}