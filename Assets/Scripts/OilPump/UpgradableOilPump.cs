using IdleTycoon.Configs;
using UnityEngine;
using Zenject;

namespace IdleTycoon.OilPump
{
    public class UpgradableOilPump : MonoBehaviour
    {
        [SerializeField] private OilPumpButtonController button;

        private float currentCost = 0f;
        private OilPumpConfig config;

        public float CurrentCost => currentCost;

        [Inject]
        private void Init(OilPumpConfig config)
        {
            this.config = config;
        }

        private void Start()
        {
            if (currentCost < config.StartUpgradeCost)
            {
                currentCost = config.StartUpgradeCost;
            }

            button.RefreshOilPumpInfo();
            RefreshPumpBar();
        }

        public void UpgradeCost()
        {
            if (GameManager.Instance.Money >= currentCost)
            {
                //TODO: связать цену с уровнем 
                config.Level++;
                config.Cost++;
                GameManager.Instance.Money -= currentCost;
                button.RefreshOilPumpInfo();
                var newCost = currentCost + currentCost * config.CostMultiplier;
                currentCost = newCost;
            }
        }

        public void UpgradeFueling()
        {
            //TODO: добавить интеректибл кнопки
            if (config.LevelPumping == 50) return;

            if (GameManager.Instance.Money >= config.CostPumping)
            {
                config.LevelPumping++;
                config.PumpingTime -= config.DecreasePumping;
                GameManager.Instance.Money -= config.CostPumping;
                config.CostPumping *= 1.8f;
                if (config.LevelPumping != 0)
                {
                    button.FuelingProgressBar.fillAmount += 1f / (50 - config.LevelPumping);
                }

                button.RefreshOilPumpInfo();
            }
        }

        public void UpgradeSpawnDelay()
        {
            if (config.LevelPumpingDelay == 50) return;

            if (GameManager.Instance.Money >= config.CostPumpingDelay)
            {
                config.LevelPumpingDelay++;
                config.PumpingDelay -= config.DecreasePumpingDelay;
                GameManager.Instance.Money -= config.CostPumpingDelay;
                config.CostPumpingDelay *= 2f;
                if (config.LevelPumpingDelay != 0)
                {
                    button.SpawnProgressBar.fillAmount += 1f / (50 - config.LevelPumpingDelay);
                }

                button.RefreshOilPumpInfo();
            }
        }

        private void RefreshPumpBar()
        {
            if (config.LevelPumping != 0)
            {
                button.FuelingProgressBar.fillAmount = 1f / (50f / config.LevelPumping);
            }

            if (config.LevelPumpingDelay != 0)
            {
                button.SpawnProgressBar.fillAmount = 1f / (50f / config.LevelPumpingDelay);
            }
        }
    }
}