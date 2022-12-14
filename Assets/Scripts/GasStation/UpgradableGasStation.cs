using IdleTycoon.Configs;
using UnityEngine;

namespace IdleTycoon.GasStation
{
    public class UpgradableGasStation : MonoBehaviour
    {
        [SerializeField] private GasStationButtonController button;
        [SerializeField] private GasStationConfig config;

        private float currentCost = 0f;
        private float currentFuelingTime;

        public float CurrentCost => currentCost;

        private void Start()
        {
            if (currentCost < config.StartUpgradeCost)
            {
                currentCost = config.StartUpgradeCost;
            }

            button.RefreshGasStationInfo();
            RefreshFuelingBar();
        }

        public void UpgradeCost()
        {
            if (GameManager.Instance.Money >= currentCost)
            {
                //TODO: связать цену с уровнем 
                config.Level++;
                config.Cost++;
                GameManager.Instance.Money -= currentCost;
                button.RefreshGasStationInfo();
                var newCost = currentCost + currentCost * config.CostMultiplier;
                currentCost = newCost;
            }
        }

        public void UpgradeFueling()
        {
            //TODO: добавить интеректибл кнопки
            if (config.LevelFueling == 50) return;

            if (GameManager.Instance.Money >= config.CostFueling)
            {
                config.LevelFueling++;
                config.FuelingTime -= config.DecreaseFueling;
                GameManager.Instance.Money -= config.CostFueling;
                config.CostFueling *= 1.8f;
                if (config.LevelFueling != 0)
                {
                    button.fuelingProgressBar.fillAmount += 1f / (50 - config.LevelFueling);
                }

                button.RefreshGasStationInfo();
            }
        }

        public void UpgradeSpawnDelay()
        {
            if (config.LevelSpawnDelay == 50) return;

            if (GameManager.Instance.Money >= config.CostSpawnDelay)
            {
                config.LevelSpawnDelay++;
                config.SpawnDelay -= config.DecreaseSpawnDelay;
                GameManager.Instance.Money -= config.CostSpawnDelay;
                config.CostSpawnDelay *= 2f;
                if (config.LevelSpawnDelay != 0)
                {
                    button.spawnProgressBar.fillAmount += 1f / (50 - config.LevelSpawnDelay);
                }

                button.RefreshGasStationInfo();
            }
        }

        private void RefreshFuelingBar()
        {
            if (config.LevelFueling != 0)
            {
                button.fuelingProgressBar.fillAmount = 1f / (50f / config.LevelFueling);
            }

            if (config.LevelSpawnDelay != 0)
            {
                button.spawnProgressBar.fillAmount = 1f / (50f / config.LevelSpawnDelay);
            }
        }
    }
}