using IdleTycoon.Ads;
using IdleTycoon.Configs;
using TMPro;
using UnityEngine;
using Zenject;

namespace IdleTycoon.GasStation
{
    public class MoneyIncreaseText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;

        private readonly Vector3 targetPositionText = new Vector3(0, 4, 0);

        private AdsController ads;
        private GasStationConfig config;

        public bool Fuel { get; set; }

        [Inject]
        public void Init(GasStationConfig config)
        {
            this.config = config;
        }

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
            text.transform.position =
                Vector3.MoveTowards(text.transform.position, targetPositionText, 2 * Time.deltaTime);
        }
    }
}