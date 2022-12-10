using System;
using System.Globalization;
using Ads;
using TMPro;
using UnityEngine;
using GasStation.Config;

namespace GasStation
{
    public class MoneyIncreaseText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private GasStationConfig config;
        private readonly Vector3 _targetPositionText = new(0, 4, 0);
        
        private AdsController _ads;

        public bool Fuel { get; set; }

        private void Awake()
        {
            _ads = FindObjectOfType<AdsController>();
        }

        private void Update()
        {
            if (Fuel)
            {
                ShowFuelText();
                text.text = "+" + FormatNums.FormatNum(config.Cost * _ads.AdvMultiplier);
            }
            else
            {
                text.text = " ";
                text.transform.position = new Vector3(0, 3, 0);
            }
        }

        private void ShowFuelText()
        {
            if (text.transform.position == _targetPositionText) Fuel = false;
            text.transform.position = Vector3.MoveTowards(text.transform.position, _targetPositionText, 2 * Time.deltaTime);
        }
    }
}