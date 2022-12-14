using IdleTycoon.Ads;
using IdleTycoon.GasStation.Config;
using TMPro;
using UnityEngine;

namespace IdleTycoon.GasStation
{
    public class MoneyIncreaseText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private GasStationConfig config;
        private readonly Vector3 targetPositionText = new Vector3(0, 4, 0);

        private AdsController ads;

        public bool Fuel { get; set; }

        private void Awake()
        {
            ads = FindObjectOfType<AdsController>();
        }

        private void Update()
        {
            if (Fuel)
            {
                ShowFuelText();
                text.text = "+" + FormatNums.FormatNum(config.Cost * ads.AdvMultiplier);
            }
            else
            {
                text.text = " ";
                text.transform.position = new Vector3(0, 3, 0);
            }
        }

        private void ShowFuelText()
        {
            if (text.transform.position == targetPositionText) Fuel = false;
            text.transform.position = Vector3.MoveTowards(text.transform.position, targetPositionText, 2 * Time.deltaTime);
        }
    }
}