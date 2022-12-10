using System;
using System.Globalization;
using GasStation;
using GasStation.Config;
using OilPump.Config;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OilPump
{
    public class OilPumpButtonController : MonoBehaviour
    {
        //TODO нужно всю инфу перевести в минуты
        //TODO все переменные через сохранения а не через конфиги, в конфигах только начальные значения.
        [SerializeField] private TextMeshProUGUI levelText= null;
        [SerializeField] private TextMeshProUGUI pumpingTimeText = null;
        [SerializeField] private TextMeshProUGUI pumpingDelay= null;
        [SerializeField] private TextMeshProUGUI profitText = null;
        [SerializeField] private TextMeshProUGUI upgradeCost = null;
        [SerializeField] private TextMeshProUGUI costFueling = null;
        [SerializeField] private TextMeshProUGUI costSpawnDelay = null;
        [SerializeField] private OilPumpConfig config;
        [SerializeField] public Image fuelingProgressBar;
        [SerializeField] public Image spawnProgressBar;

        private UpgradableOilPump _upgradableOilPump;

        private void Awake()
        {
            _upgradableOilPump = FindObjectOfType<UpgradableOilPump>();
        }

        private void Start()
        {
            RefreshOilPumpInfo();
        }

        //TODO: либо добавить строчку про спавн машин, либо заменить машин в минуту
        public void RefreshOilPumpInfo()
        {
            levelText.text = "Уровень: " + config.Level;
            pumpingTimeText.text = "Время добычи: " + Math.Round(config.PumpingTime, 2).ToString(CultureInfo.InvariantCulture) + "c";
            pumpingDelay.text = "добыча в минуту: " + Math.Round(60f / config.PumpingTime , 1).ToString(CultureInfo.InvariantCulture);
            profitText.text = FormatNums.FormatNum(config.Cost);
            upgradeCost.text = FormatNums.FormatNum(_upgradableOilPump.CurrentCost);
            costFueling.text = FormatNums.FormatNum(config.CostPumping);
            costSpawnDelay.text = FormatNums.FormatNum(config.CostPumpingDelay);
        }
    }
}