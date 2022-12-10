using OilPump.Config;
using UnityEngine;

namespace OilPump
{
    public class UpgradableOilPump : MonoBehaviour
    {
        [SerializeField] private OilPumpButtonController button;
        [SerializeField] private OilPumpConfig config;
        private float _currentCost = 0f;

        public float CurrentCost => _currentCost;

        private void Start()
        {
            if (_currentCost < config.StartUpgradeCost)
            {
                _currentCost = config.StartUpgradeCost;
            }
            
            button.RefreshOilPumpInfo();
            RefreshPumpBar();
        }
        
        public void UpgradeCost()
        {
            if (GameManager.Instance.Money >= _currentCost)
            {
                //TODO: связать цену с уровнем 
                config.Level++;
                config.Cost++;
                GameManager.Instance.Money -= _currentCost;
                button.RefreshOilPumpInfo();
                var newCost = _currentCost + _currentCost * config.CostMultiplier;
                _currentCost = newCost;
            }
        }
        
        public void UpgradeFueling()
        {
            //TODO: добавить интеректибл кнопки
            if(config.LevelPumping == 50) return;

            if (GameManager.Instance.Money >= config.CostPumping)
            {
                config.LevelPumping++;
                config.PumpingTime -= config.DecreasePumping;
                GameManager.Instance.Money -= config.CostPumping;
                config.CostPumping *= 1.8f;
                if (config.LevelPumping != 0)
                {
                    button.fuelingProgressBar.fillAmount += 1f / (50 - config.LevelPumping);
                }
                button.RefreshOilPumpInfo();
            }
        }

        public void UpgradeSpawnDelay()
        {
            if(config.LevelPumpingDelay == 50) return;
            
            if (GameManager.Instance.Money >= config.CostPumpingDelay)
            {
                config.LevelPumpingDelay++;
                config.PumpingDelay -= config.DecreasePumpingDelay;
                GameManager.Instance.Money -= config.CostPumpingDelay;
                config.CostPumpingDelay *= 2f;
                if (config.LevelPumpingDelay != 0)
                {
                    button.spawnProgressBar.fillAmount += 1f / (50 - config.LevelPumpingDelay);
                }
                button.RefreshOilPumpInfo();
            }
        }

        private void RefreshPumpBar()
        {
            if (config.LevelPumping != 0)
            {
                button.fuelingProgressBar.fillAmount = 1f / (50f / config.LevelPumping);
            }
            
            if (config.LevelPumpingDelay != 0)
            {
                button.spawnProgressBar.fillAmount = 1f / (50f / config.LevelPumpingDelay);
            }
        }
    }
}