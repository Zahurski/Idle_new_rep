using System;
using System.Globalization;
using Ads;
using Components;
using TMPro;
using UnityEngine;
using OilPump.Config;

namespace OilPump
{
    public class OilPumpMoneyIncreaseText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private OilPumpConfig config;
        private Vector3 _targetPositionText;
        private OilPumpComponent _oilPump;
        private AdsController _ads;

        public bool Pump { get; set; }

        private void Awake()
        {
            _oilPump = FindObjectOfType<OilPumpComponent>();
            _ads = FindObjectOfType<AdsController>();
        }

        private void Start()
        {
            _targetPositionText = new Vector3(_oilPump.transform.position.x, 4, _oilPump.transform.position.z);
        }

        private void Update()
        {
            if (Pump)
            {
                ShowFuelText();
                text.text = "+" + FormatNums.FormatNum(config.Cost * _ads.AdvMultiplier);
            }
            else
            {
                text.text = " ";
                text.transform.position = new Vector3(_oilPump.transform.position.x, 3, _oilPump.transform.position.z);
            }
        }

        private void ShowFuelText()
        {
            if (text.transform.position == _targetPositionText) Pump = false;
            text.transform.position = Vector3.MoveTowards(text.transform.position, _targetPositionText, 2 * Time.deltaTime);
        }
    }
}