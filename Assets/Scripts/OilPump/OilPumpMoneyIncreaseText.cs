using IdleTycoon.Ads;
using IdleTycoon.Components;
using IdleTycoon.Configs;
using TMPro;
using UnityEngine;
using Zenject;

namespace IdleTycoon.OilPump
{
    public class OilPumpMoneyIncreaseText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;

        private Vector3 targetPositionText;
        private OilPumpComponent oilPump;
        private AdsController ads;

        private OilPumpConfig config;

        public bool Pump { get; set; }

        [Inject]
        private void Init(OilPumpConfig config)
        {
            this.config = config;
        }

        private void Awake()
        {
            oilPump = FindObjectOfType<OilPumpComponent>();
            ads = FindObjectOfType<AdsController>();
        }

        private void Start()
        {
            targetPositionText = new Vector3(oilPump.transform.position.x, 4, oilPump.transform.position.z);
        }

        private void Update()
        {
            if (Pump)
            {
                ShowFuelText();
                text.text = "+" + FormatNums.FormatNum(config.Cost * ads.AdditionalMultiplier);
            }
            else
            {
                text.text = " ";
                text.transform.position = new Vector3(oilPump.transform.position.x, 3, oilPump.transform.position.z);
            }
        }

        private void ShowFuelText()
        {
            if (text.transform.position == targetPositionText) Pump = false;
            text.transform.position =
                Vector3.MoveTowards(text.transform.position, targetPositionText, 2 * Time.deltaTime);
        }
    }
}