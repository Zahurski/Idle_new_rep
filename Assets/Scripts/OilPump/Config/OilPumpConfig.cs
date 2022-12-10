using UnityEngine;

namespace OilPump.Config
{
    [CreateAssetMenu(fileName = "OilPumpConfig", menuName = "Configs/OilPumpConfig", order = 0)]
    public class OilPumpConfig : ScriptableObject
    {
        [SerializeField] private float startUpgradeCost = 5f;
        [SerializeField] private float costMultiplier = 0.06f;
        [SerializeField] private float cost = 5f;
        [SerializeField] private float costPumping = 5f;
        [SerializeField] private float costPumpingDelay = 10f;
        [SerializeField] private float pumpingDelay = 5f;
        [SerializeField] private float pumpingTime = 5f;
        [SerializeField] private int level = 1;
        [SerializeField] private int levelPumping = 0;
        [SerializeField] private int levelPumpingDelay = 0;

        private float _decreasePumping = 0.05f;
        private float _decreasePumpingDelay = 0.05f;
        public float StartUpgradeCost => startUpgradeCost;
        public float CostMultiplier => costMultiplier;

        public int Level
        {
            get => level;
            set => level = value;
        }

        public float Cost
        {
            get => cost;
            set => cost = value;
        }

        public float CostPumping
        {
            get => costPumping;
            set => costPumping = value;
        }

        public float CostPumpingDelay
        {
            get => costPumpingDelay;
            set => costPumpingDelay = value;
        }

        public float DecreasePumping => _decreasePumping;

        public float DecreasePumpingDelay => _decreasePumpingDelay;

        public float PumpingDelay
        {
            get => pumpingDelay;
            set => pumpingDelay = value;
        }

        public float PumpingTime
        {
            get => pumpingTime;
            set => pumpingTime = value;
        }

        public int LevelPumping
        {
            get => levelPumping;
            set => levelPumping = value;
        }

        public int LevelPumpingDelay
        {
            get => levelPumpingDelay;
            set => levelPumpingDelay = value;
        }
    }
}